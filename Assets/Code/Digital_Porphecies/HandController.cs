using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


namespace Digital_Porphecies {
    public class HandController : MonoBehaviour {

        public GameObject handPrefab;

        public InputDeviceCharacteristics inputDeviceCharacteristics;

        InputDevice _targetDevice;
        Animator _handAnimator;

        
        // Start is called before the first frame update
        void Start() {
            InitializeHand();
            

        }



        void InitializeHand() {
            List<InputDevice> devices = new List<InputDevice>();
            
            InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

            if (devices.Count > 0) {
                _targetDevice = devices[0];

                GameObject spawnedHand = Instantiate(handPrefab, transform);
                _handAnimator = spawnedHand.GetComponent<Animator>();
            }
        }
        
        // Update is called once per frame
        void Update() {
            if (!_targetDevice.isValid) {
                InitializeHand();
            }
            else {
                UpdateHand();
            }
        }

        void UpdateHand() {
            if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && _targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
                float closeValue = Mathf.Max(gripValue, triggerValue);
                _handAnimator.SetFloat("Grip", closeValue);
            }
            else {
                _handAnimator.SetFloat("Grip", 0);
            }
        }
    }
}
//Thanks to the Fist Full of Shrimp Youtube Channel