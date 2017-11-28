using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPoints : MonoBehaviour {

    [SerializeField]
    private Transform newPos;

	void OnTriggerEnter(Collider col) {
        col.gameObject.GetComponent<NavTarget>().target = newPos;
        col.gameObject.GetComponent<NavMeshAgent>().speed = Random.Range(5f, 9f);

    }
}
