using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing {
    public class CameraController : MonoBehaviour {

        public float mouseSensitivity = 100f;

        private float _xCamRot = 0f;

        private float _yCamRot = 0f;

        // Start is called before the first frame update
        void Start() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update() {
            _xCamRot -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            _xCamRot = Mathf.Clamp(_xCamRot, -90f, 90f);

            _yCamRot += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(_xCamRot, _yCamRot, 0f);
        }
    }
}