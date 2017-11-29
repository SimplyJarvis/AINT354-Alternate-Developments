using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour {

    public GameObject win;

    private void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            win.SetActive(true);
            Instantiate(win);
            Debug.Log("winner");
            Time.timeScale = (0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        win.SetActive(false);
        Destroy(win);
        Debug.Log("RunAway");
        Time.timeScale = (1);
    }

}
