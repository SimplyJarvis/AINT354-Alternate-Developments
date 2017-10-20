using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comabt_BulletMove : MonoBehaviour {
    [SerializeField]
    int movementSpeed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }
}
