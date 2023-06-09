using System;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine;
using UnityEngine.Networking;


namespace Digital_Porphecies {
    public class WebServerHandler : MonoBehaviour {
        public static WebServerHandler instance;

        public TTSSpeaker tts;
        
        bool _hasEverStarted;


        void Awake() {
            instance = this;
        }

        // Start is called before the first frame update
        void Start() {
            // StartCoroutine(GetRequest("http://localhost:8080"));
            // StartCoroutine(Upload());

            _hasEverStarted = false;
        }

        void Update() {
            if (!_hasEverStarted && WorldManager.instance.isOracleActive) {
                _hasEverStarted = true;
                ClearChat();
            }
        }

        public IEnumerator GetRequest(string uri) {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
                //Request and wait for the desired page
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                print("RESULT : " + webRequest.result);

                switch (webRequest.result) {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                        break;
                }
            }
        }

        public IEnumerator Upload(string userInput) {
            WWWForm form = new WWWForm();
            form.AddField("", userInput);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080", form)) {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.LogError(www.error);
                }
                else {
                    //GOT SUCCESSFUL TEXT BACK!!
                    Debug.Log(www.downloadHandler.text);
                    tts.Speak(www.downloadHandler.text);
                }
            }
        }

        public void ClearChat() {
            StartCoroutine(Upload("AdminResetChatLog"));
        }

        public void SendToServer(string userInput) {
            if (WorldManager.instance.isOracleActive) {

                Debug.Log("SENDING TO SERVER");
                StartCoroutine(Upload(userInput));
            }
            else {
                print("ORACLE NOT ACTIVE");
            }
        }
    }
}