using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildScript : MonoBehaviour {

    private GameObject tank;

	// Use this for initialization
	void Start () {        
        tank = GameObject.Find("sheildTank");
        this.transform.parent = tank.transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(tank.transform.position, Vector3.up, 100f * Time.deltaTime);
	}
}
