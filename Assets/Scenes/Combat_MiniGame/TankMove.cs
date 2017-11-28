using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour {

    private Vector3 originalPos;
    private Vector3 newPos;
    private GameObject Tank;

	// Use this for initialization
	void Start () {
        Tank = this.gameObject;
        originalPos = transform.position;
        newPos = originalPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SlerpFunction();
	}


    public void Move(string direction)
    {
        if (direction.Contains("up"))
        {
            newPos = transform.position + transform.forward * 1.5f;
        }
        else if (direction.Contains("down"))
        {
            newPos = transform.position - transform.forward * 1.5f;
        }
        else if (direction.Contains("left"))
        {
            newPos = transform.position - transform.right * 1.5f;
        }
        else if (direction.Contains("right"))
        {
            newPos = transform.position + transform.right * 1.5f;
        }
    }
     public void SlerpFunction()
    {

        originalPos = Tank.transform.position;
      


        if (originalPos != newPos)
        {
            originalPos = Vector3.Lerp(originalPos, newPos, Time.deltaTime * 1f);
            Tank.transform.position = originalPos;
        }




    }







}
