using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

    public GameObject win;
    

    private void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            //win.SetActive(true);
           // Instantiate(win);
            Debug.Log("winner");
             Invoke("Restart", 3f);
           // Restart();
        }
    }

   

    void Restart()
    {
        Debug.Log("scenchange");
        SceneManager.LoadScene("MazeWin");

    }

}
