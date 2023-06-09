using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Testing {
    public class NewBehaviourScript : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            StartCoroutine(GetRequest("http://localhost:8080"));
            StartCoroutine(Upload());
        }

        IEnumerator GetRequest(string uri) {
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

        IEnumerator Upload() {
            WWWForm form = new WWWForm();
            form.AddField("", "Do you like tacos?");

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080", form)) {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.LogError(www.error);
                }
                else {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}