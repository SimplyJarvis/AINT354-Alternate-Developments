using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Player : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Combat_ChatIRC>().DeathCount();
            Destroy(gameObject);
        }
        
    }
	
}
