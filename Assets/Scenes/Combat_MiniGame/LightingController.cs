using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour {

    private Quaternion newPos;
    private Quaternion originalPos;
    public float speed = 0.1f;
    private double timer = 0;
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    // Use this for initialization
    void Start () {
        newPos = Quaternion.Euler(Random.Range(0.1f, 180.0f), Random.Range(0.1f, 360.0f), Random.Range(0.1f, 360.0f));
        originalPos = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        lerpLights();


    }

    void lerpLights()
    {
        transform.rotation = Quaternion.Slerp(originalPos, newPos, Time.deltaTime * speed);
        originalPos = transform.rotation;

        if (timer > Random.Range(2.0f, 4.0f))
        {


            newPos = Quaternion.Euler(Random.Range(0.1f, 180.0f), Random.Range(0.1f, 360.0f), Random.Range(0.1f, 360.0f));
            
            timer = 0;
        }
        timer += Time.deltaTime;

        
    } 
    
    

}
