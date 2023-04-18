using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    public float speed;

    Rigidbody _rb;
    

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
        // _rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        
        if (Input.GetKey(KeyCode.W)) {
            transform.position += (Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) {
            transform.position += (Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.position += (Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position += (Vector3.right * speed * Time.deltaTime);
        }
    }
}