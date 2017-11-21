using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class VotingGame : MonoBehaviour
{

    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;
    public int maxMessagesVote = 100;
    private LinkedList<string> voted = new LinkedList<string>();
    private int maze = 0;
    private int tank = 0;
    private int guess = 0;
    private int typing = 0;
    public Text[] tallyVotes = new Text[4];
    private int timeRemaining = 30;
    public Text counting;
    private int[] chosen = new int[4];
    private int chosenGame;


    // Use this for initialization
    void Start()
    {
        IRC = GameObject.Find("Twitch").GetComponent<TwitchIRC>();
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
        StartCoroutine("Countdown");
        PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        tallyVotes[0].text = "Votes for Maze game: " + maze;
        tallyVotes[1].text = "Votes for Tank game: " + tank;
        tallyVotes[2].text = "Votes for Guessing game: " + guess;
        tallyVotes[3].text = "Votes for Typing game: " + typing;
        counting.text = "Time until next game: " + timeRemaining;

        if (timeRemaining == 0)
        {
            chosen[0] = maze;
            chosen[1] = tank;
            chosen[2] = guess;
            chosen[3] = typing;

            List<int> tooRemove = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (chosen[i] == chosen.Max())
                {
                    tooRemove.Add(i);
                }
            }
            chosenGame = tooRemove[Random.Range(0, tooRemove.Count)];
            if (chosenGame == 0)
            {
                SceneManager.LoadScene("Scene");
            }
            if (chosenGame == 1)
            {
                SceneManager.LoadScene("Combat_MiniGame");
            }
            if (chosenGame == 2)
            {
                SceneManager.LoadScene("Catchphrase");
            }
            if (chosenGame == 3)
            {
                SceneManager.LoadScene("MainTest");
            }
        }
    }


    void OnChatMsgRecieved(string msg)
    {
        bool hasVoted = false;
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
        string user = msg.Substring(1, msg.IndexOf('!') - 1);

        if (messages.Count > maxMessages)
        {
            Destroy(messages.First.Value);
            messages.RemoveFirst();
        }

        if (msgString.ToLower().Contains("maze"))
        {
            if (voted.Contains(user))
            {
                hasVoted = true;
            }
            if (hasVoted == false)
            {
                maze++;
                voted.AddLast(user);
            }
        }

        if (msgString.ToLower().Contains("tank"))
        {
            if (voted.Contains(user))
            {
                hasVoted = true;
            }
            if (hasVoted == false)
            {
                tank++;
                voted.AddLast(user);
            }
        }

        if (msgString.ToLower().Contains("guessing"))
        {
            if (voted.Contains(user))
            {
                hasVoted = true;
            }
            if (hasVoted == false)
            {
                guess++;
                voted.AddLast(user);
            }
        }

        if (msgString.ToLower().Contains("typing"))
        {
            if (voted.Contains(user))
            {
                hasVoted = true;
            }
            if (hasVoted == false)
            {
                typing++;
                voted.AddLast(user);
            }
        }

        if (voted.Count > maxMessagesVote)
        {
            voted.RemoveFirst();
        }


    }

    IEnumerator Countdown()
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
    }

}
