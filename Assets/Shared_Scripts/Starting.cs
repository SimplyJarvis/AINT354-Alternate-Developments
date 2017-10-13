using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starting : MonoBehaviour {

    public GameObject loading;
    public GameObject textBoxActive;
    public InputField textBox;
    public GameObject button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void IsPushed()
    {
        loading.GetComponent<TwitchIRC>().oauth = textBox.text;
        loading.SetActive(true);
        textBoxActive.SetActive(false);
        button.SetActive(false);
    }
}
