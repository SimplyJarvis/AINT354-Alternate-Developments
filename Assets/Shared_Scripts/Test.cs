﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TwitchIRC))]
public class Test : MonoBehaviour {

    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;
    public Text test;
    public Text test2;
    private bool notWon = false;
    public GameObject boxey;

    // Use this for initialization
    void Start () {
        IRC = this.GetComponent<TwitchIRC>();
        //IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnChatMsgRecieved(string msg)
    {
        //parse from buffer.
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
        string user = msg.Substring(1, msg.IndexOf('!') - 1);

        //remove old messages for performance reasons.
        if (messages.Count > maxMessages)
        {
            Destroy(messages.First.Value);
            messages.RemoveFirst();
        }
        //text game
        if (user == "simply_jarvis" & msgString == "!start")
        {
            test.enabled = true;
            test2.text = "Type now!!!";
        }
        if (msgString == "Bibble, this word makes no sense" & notWon == false){
            test2.text = "Winner: " + user;
            notWon = true;
        }
        //end text game

        //move cube
        if (msgString == "Move left")
        {
            Vector3 position = boxey.transform.position;
            position.x += -1;
            boxey.transform.position = position;
        }
        else if (msgString == "Move right")
        {
            Vector3 position = boxey.transform.position;
            position.x += 1;
            boxey.transform.position = position;
        }
        else if (msgString == "Move down")
        {
            Vector3 position = boxey.transform.position;
            position.y += -1;
            boxey.transform.position = position;
        }
        else if (msgString == "Move up")
        {
            Vector3 position = boxey.transform.position;
            position.y += 1;
            boxey.transform.position = position;
        }
        //end move cube

        //colour cube
        if (msgString.Contains("!cubecolour"))
        {
            boxey.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
        //end colour cube

        //test.text = user;

        Debug.Log(user + " : " + msgString);

        ////add new message.
        //CreateUIMessage(user, msgString);
    }
}