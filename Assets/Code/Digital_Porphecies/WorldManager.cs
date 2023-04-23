using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public void ResetScene() {
        SceneManager.LoadScene("Digital_Prophecies");
    }
}
