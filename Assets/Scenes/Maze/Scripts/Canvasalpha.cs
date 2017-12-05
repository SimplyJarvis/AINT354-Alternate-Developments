using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvasalpha : MonoBehaviour {
    float fade;

    public RawImage screen;
	// Use this for initialization
	void Start () {

        fade = 0.01f;

    }

 
	
	// Update is called once per frame
	void Update () {

        fade = fade + 0.01f;
       

        screen.CrossFadeAlpha(fade, 30f, true);

        


    }
}
