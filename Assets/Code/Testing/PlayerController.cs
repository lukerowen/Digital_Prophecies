using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Testing {
    public class PlayerController : MonoBehaviour {
        public float speed;

        Rigidbody _rb;
        public Transform cameraTransform;

        // Start is called before the first frame update
        void Start() {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update() {
            
            if (Input.GetKey(KeyCode.W)) {
                transform.position += (cameraTransform.forward * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S)) {
                transform.position += (-cameraTransform.forward * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A)) {
                transform.position += (-cameraTransform.right * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D)) {
                transform.position += (cameraTransform.right * speed * Time.deltaTime);
            }
        }
    }
}