using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Player : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Combat_ChatIRC>().DeathCount();
            Destroy(gameObject);
        
    }
	
}
