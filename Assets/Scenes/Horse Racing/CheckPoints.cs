using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPoints : MonoBehaviour {

    [SerializeField]
    private Transform[] newPos;

	void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Racer")
        {
            col.gameObject.GetComponent<NavTarget>().target = newPos[Random.Range(0,2)];
            col.gameObject.GetComponent<NavMeshAgent>().speed = Random.Range(3.5f, 5.5f);
        }

    }
}
