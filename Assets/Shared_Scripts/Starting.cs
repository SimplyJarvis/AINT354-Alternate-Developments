using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour {

    public GameObject loading;
    public InputField textBoxOauth;
    public InputField textBoxUser;
    public GameObject button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void IsPushed()
    {
        loading.GetComponent<TwitchIRC>().oauth = textBoxOauth.text;
        loading.GetComponent<TwitchIRC>().channelName = textBoxUser.text.ToLower();
        loading.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
}
