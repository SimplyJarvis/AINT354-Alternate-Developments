using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    GameObject raceCam;
    // Use this for initialization
    void Start()
    {
        Invoke("startRace", 10f);
        
    }

    void startRace()
    {
        raceCam.SetActive(true);
        gameObject.SetActive(false);
    }
}
