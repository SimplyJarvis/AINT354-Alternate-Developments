using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TwitchIRC))]
public class Test : MonoBehaviour {

    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;
    public Text test;
    public Text test2;
    public TextMeshProUGUI Fancytext1;
    private bool notWon = false;
    private string[] textFile;
    private int randomNum;
    private bool newGame = false;
    private int timeRemaining = 10;
    private int totalGames = 0;
    private int maxGames = 5;

    // Use this for initialization
    void Start () {
        IRC = GameObject.Find("Twitch").GetComponent<TwitchIRC>();
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
        if (newGame == false)
        {
           // test.enabled = true;
            var sr = File.OpenText("Assets/Games_Scripts/Phrases.txt");
            textFile = sr.ReadToEnd().Split("\n"[0]);
            randomNum = Random.Range(0,textFile.Length);
            test.text = textFile[randomNum].Substring(0, textFile[randomNum].Length -1);
            //TextMeshPro fancytext1 = GetComponent<TextMeshPro>();
            Fancytext1.SetText(test.text);
            notWon = false;
            newGame = true;
        }
        if (msgString.ToLower().Contains(test.text.ToLower()) & notWon == false){
            Debug.Log("----------------------------");
            test2.text = "Winner: " + user;
            notWon = true;
            Fancytext1.SetText(test2.text);
            StartCoroutine("NewRound");
        }       

        if (timeRemaining == 0)
        {
            newGame = false;
            timeRemaining = 10;
        }

        Debug.Log(user + " : " + msgString);
        Debug.Log(msgString);
        Debug.Log(test.text);

        ////add new message.
        //CreateUIMessage(user, msgString);
    }

    IEnumerator NewRound()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            if (timeRemaining == 0)
            {
                break;
            }
        }
        totalGames++;
        if (totalGames == maxGames)
        {
            SceneManager.LoadScene("ChooseGame");
        }
    }
}
