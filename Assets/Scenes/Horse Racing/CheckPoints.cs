﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPoints : MonoBehaviour {

    [SerializeField]
    private Transform newPos;

	void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Racer")
        {
            col.gameObject.GetComponent<NavTarget>().target = newPos;
            col.gameObject.GetComponent<NavMeshAgent>().speed = Random.Range(4f, 5.5f);
        }

    }
}
