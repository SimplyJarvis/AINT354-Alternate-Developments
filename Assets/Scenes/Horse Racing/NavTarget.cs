using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTarget : MonoBehaviour {

    public Transform target;
    NavMeshAgent navTest;

	// Use this for initialization
	void Start () {
        navTest = GetComponent<NavMeshAgent>();
        navTest.speed = (Random.Range(3.5f, 5.5f));
	}
	
	// Update is called once per frame
	void Update () {
        navTest.SetDestination(target.position);
	}
}
