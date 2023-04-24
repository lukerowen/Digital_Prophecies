using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Windows.Speech;


namespace Digital_Porphecies {
    public class InputController : MonoBehaviour {
        List<InputDevice> _inputDevices = new List<InputDevice>();

        DictationRecognizer _recognizer;

        bool _isHeld = false;

        // Start is called before the first frame update
        void Start() {
            _recognizer = new DictationRecognizer();
            _recognizer.DictationResult += RecognizerOnDictationResult;
            _recognizer.DictationComplete += RecognizerOnDictationComplete;
            // _recognizer.DictationHypothesis += RecognizerOnDictationHypothesis;
            _recognizer.DictationError += RecognizerOnDictationError;
        }

        private void RecognizerOnDictationError(string error, int hresult) {
            Debug.LogError(error);
        }

        void RecognizerOnDictationComplete(DictationCompletionCause cause) {
            _recognizer.Stop();
        }

        void RecognizerOnDictationResult(string text, ConfidenceLevel confidence) {
            Debug.Log(text);
            WebServerHandler.instance.SendToServer(text);
        }

        void InitializeInputReader() {
            // InputDevices.GetDevices(_inputDevices);
            InputDevices.GetDevicesWithCharacteristics(
                InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, _inputDevices);

            foreach (var inputDevice in _inputDevices) {
                inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out var value);
                Debug.Log(inputDevice.name + " " + value);

                //Debug.Log(inputDevice.name + " " + inputDevice.characteristics);
            }
        }

        // Update is called once per frame
        void Update() {
            InputDevices.GetDevicesWithCharacteristics(
                InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, _inputDevices);

            foreach (var inputDevice in _inputDevices) {
                inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed);
                // Debug.Log(inputDevice.name + " " + value);

                if (_recognizer.Status != SpeechSystemStatus.Running && isPressed && !_isHeld) {
                    Debug.Log("STARTING");
                    _recognizer.Start();

                    _isHeld = true;
                }

                if (!isPressed) {
                    _isHeld = false;
                }
            }
        }
    }
}

//Thanks to the Fist Full of Shrimp Youtube Channel