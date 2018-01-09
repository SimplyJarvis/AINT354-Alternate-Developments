using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour {

    private Vector3 originalPos;
    private Vector3 newPos;
    private Vector3 bouncePos;
    private GameObject Tank;
    private Combat_BulletSpawn bullets;

	// Use this for initialization
	void Start () {
        Tank = this.gameObject;
        originalPos = transform.position;
        newPos = originalPos;
        bullets = GetComponentInChildren<Combat_BulletSpawn>();
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
            bouncePos = Tank.transform.position;
        }
        else if (direction.Contains("down"))
        {
            newPos = transform.position - transform.forward * 1.5f;
            bouncePos = Tank.transform.position;
        }
        else if (direction.Contains("left"))
        {
            newPos = transform.position - transform.right * 1.5f;
            bouncePos = Tank.transform.position;
        }
        else if (direction.Contains("right"))
        {
            newPos = transform.position + transform.right * 1.5f;
            bouncePos = Tank.transform.position;
        }
    }
     public void SlerpFunction()
    {

        originalPos = Tank.transform.position;
      


        if (originalPos != newPos)
        {
            if (newPos.z < 4.35 && newPos.z > -4.35 && newPos.x < 7.4 && newPos.x > -7.4)
            {
                originalPos = Vector3.Lerp(originalPos, newPos, Time.deltaTime * 1f);
                Tank.transform.position = originalPos;
            }
            else
            {
                
                Invoke("Bounce", 1f);
                originalPos = Vector3.Lerp(originalPos, newPos, Time.deltaTime * 1f);
                Tank.transform.position = originalPos;
            }
        }




    }

    private void Bounce()
    {
        newPos = bouncePos;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "pUp")
        {
            bullets.powerUp = true;
            Destroy(collision.gameObject);

        }
    }







}
