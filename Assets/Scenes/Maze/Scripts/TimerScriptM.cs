using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScriptM : MonoBehaviour {
    public float timeleft;
    // Use this for initialization
    void Start () {
        timeleft = 20.0f;
       
	}
	
	// Update is called once per frame
	void Update () {

        timeleft = timeleft - Time.deltaTime;

        if (timeleft < 0) {
            SceneManager.LoadScene("ChooseGame");
        }
	}
}
