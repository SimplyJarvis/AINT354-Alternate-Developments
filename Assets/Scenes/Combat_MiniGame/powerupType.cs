using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupType : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int powerUp = Random.Range(1, 4);
        Renderer rend = GetComponent<Renderer>();
        if (powerUp == 1)
        {
            rend.material.color = Color.blue;
            rend.material.SetColor("_EmissionColor", Color.blue);
        }
        if (powerUp == 2)
        {
            rend.material.color = Color.red;
            rend.material.SetColor("_EmissionColor", Color.red);
        }
        if (powerUp == 3)
        {
            rend.material.color = Color.green;
            rend.material.SetColor("_EmissionColor", Color.green);
        }
        transform.gameObject.tag = "pUp"+powerUp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
