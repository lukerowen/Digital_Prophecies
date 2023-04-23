using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Digital_Porphecies {
    public class InputController : MonoBehaviour {
        private List<InputDevice> _inputDevices = new List<InputDevice>();
        
        // Start is called before the first frame update
        void Start() {
            InitializeInputReader();
        }

        void InitializeInputReader() {
            // InputDevices.GetDevices(_inputDevices);
            InputDevices.GetDevicesWithCharacteristics( InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller ,_inputDevices);

            foreach (var inputDevice in _inputDevices) {
                inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
                Debug.Log(inputDevice.name + " " + rotation);
                
                //Debug.Log(inputDevice.name + " " + inputDevice.characteristics);
            }
        }
        
        // Update is called once per frame
        void Update() {
            if (_inputDevices.Count < 2) {
                InitializeInputReader();
            }
        }
    }
}

//Thanks to the Fist Full of Shrimp Youtube Channel