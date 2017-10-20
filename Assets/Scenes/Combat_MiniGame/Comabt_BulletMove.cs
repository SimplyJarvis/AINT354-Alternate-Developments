using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comabt_BulletMove : MonoBehaviour {
    [SerializeField]
    int movementSpeed = 5;
    double timer = 0;
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }
}
