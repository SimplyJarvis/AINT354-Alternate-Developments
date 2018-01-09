using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGun : MonoBehaviour {
    [SerializeField]
    TextMesh text;
    NavTarget navtarget;

	// Use this for initialization
	void Start () {
        Invoke("startRace", 10f);
        navtarget = GetComponent<NavTarget>();
	}

    void startRace()
    {
        text.text = "";
        navtarget.enabled = true;
    }
}
