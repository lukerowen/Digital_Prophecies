using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Digital_Porphecies {
    public class WorldManager : MonoBehaviour {

        public static WorldManager instance;

        public Transform player;
        public Transform oracleTransform;

        public GameObject oracle;

        public float oracleRange;

        public bool isOracleActive;

        void Awake() {
            instance = this;
        }

        void Start() {
            isOracleActive = IsPlayerCloseEnough();
        }

        void Update() {
            isOracleActive = IsPlayerCloseEnough();
            
            oracle.SetActive(isOracleActive);
        }

        public void ResetScene() {
            SceneManager.LoadScene("Digital_Prophecies");
        }

        bool IsPlayerCloseEnough() {
            return Vector3.Distance(player.position, oracleTransform.position) < oracleRange;
        }
    }
}