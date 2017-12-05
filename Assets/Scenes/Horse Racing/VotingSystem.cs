using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VotingSystem : MonoBehaviour {

    public Text victory;
    private int finishPlace = 0;
    public GameObject finshingStanding;
    public Text votedHorses;
    public GameObject allWinnersObject;
    public Text allWinners;
    private string winningHorse;
    public GameObject background1;
    public GameObject background2;
    public GameObject endObject;
    public Text endText;

    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;

    private int[] votes = new int[5];
    private LinkedList<string> nameVote = new LinkedList<string>();
    private List<string> nameVoteGlue= new List<string>();
    private List<string> nameVoteKart = new List<string>();
    private List<string> nameVoteFab = new List<string>();
    private List<string> nameVoteTom = new List<string>();
    private List<string> nameVoteSnek = new List<string>();

    // Use this for initialization
    void Start () {
        IRC = GameObject.FindGameObjectWithTag("ControllerIRC").GetComponent<TwitchIRC>();
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
        StopCoroutine("NewGame");
    }
	
	// Update is called once per frame
	void Update () {
        votedHorses.text = "Votes for Winners:" + "\n";
        votedHorses.text = votedHorses.text + "Glue: " + votes[0] + "\n";
        votedHorses.text = votedHorses.text + "Wunder Kart: " + votes[1] + "\n";
        votedHorses.text = votedHorses.text + "Prefabulous: " + votes[2] + "\n";
        votedHorses.text = votedHorses.text + "Le Thomas: " + votes[3] + "\n";
        votedHorses.text = votedHorses.text + "Snek: " + votes[4] + "\n";
    }

    void OnChatMsgRecieved(string msg)
    {
        //parse from buffer.
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11).ToLower();
        string user = msg.Substring(1, msg.IndexOf('!') - 1);

        //remove old messages for performance reasons.
        if (messages.Count > maxMessages)
        {
            Destroy(messages.First.Value);
            messages.RemoveFirst();
        }
        if (msgString.ToLower() == "glue" & !nameVote.Contains(user))
        {
            votes[0]++;
            nameVote.AddLast(user);
            nameVoteGlue.Add(user);
        }
        if (msgString.ToLower() == "wunder kart" & !nameVote.Contains(user))
        {
            votes[1]++;
            nameVote.AddLast(user);
            nameVoteKart.Add(user);
        }
        if (msgString.ToLower() == "prefabulous" & !nameVote.Contains(user))
        {
            votes[2]++;
            nameVote.AddLast(user);
            nameVoteFab.Add(user);
        }
        if (msgString.ToLower() == "le thomas" & !nameVote.Contains(user))
        {
            votes[3]++;
            nameVote.AddLast(user);
            nameVoteTom.Add(user);
        }
        if (msgString.ToLower() == "snek" & !nameVote.Contains(user))
        {
            votes[4]++;
            nameVote.AddLast(user);
            nameVoteSnek.Add(user);
        }


    }



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Racer")
        {
            finshingStanding.SetActive(true);
            finishPlace++;
            victory.text = victory.text + "\n" + finishPlace + ": " + col.gameObject.name;
            if (finishPlace == 1)
            {
                winningHorse = col.gameObject.name;
            }
        }
        if (finishPlace == 1)
        {
            Debug.Log(winningHorse);
            allWinnersObject.SetActive(true);
            background1.SetActive(true);
            allWinners.text = "Winner: " + winningHorse + "\n";
            allWinners.text = allWinners.text + "Congratulations";
            if (winningHorse == "Glue")
            {
                foreach (string str in nameVoteGlue)
                {
                    allWinners.text = allWinners.text + ", " + str;
                }
            }
            else if (winningHorse == "Wunder Kart")
            {
                foreach (string str in nameVoteKart)
                {
                    allWinners.text = allWinners.text + ", " + str;
                }
            }
            else if (winningHorse == "Prefabulous")
            {
                foreach (string str in nameVoteFab)
                {
                    allWinners.text = allWinners.text + ", " + str;
                }
            }
            else if (winningHorse == "Snek")
            {
                foreach (string str in nameVoteSnek)
                {
                    allWinners.text = allWinners.text + ", " + str;
                }
            }
            else if (winningHorse == "Le Thomas")
            {
                foreach (string str in nameVoteTom)
                {
                    allWinners.text = allWinners.text + ", " + str;
                }
            }
            StartCoroutine("NewGame");
            
        }
        

    }

    IEnumerator NewGame()
    {
        int timeRemaining = 15;
        background2.SetActive(true);
        endObject.SetActive(true);
        

        
        if (PlayerPrefs.GetInt("Score") == 1)
        {
            for (; ; )
            {
                endText.text = "Back to Main Menu in: " + timeRemaining;
                yield return new WaitForSeconds(1f);
                timeRemaining--;
                if (timeRemaining == 0)
                {
                    break;
                }
            }
            PlayerPrefs.SetInt("Score", 0);
            SceneManager.LoadScene("ChooseGame");
        }
        else
        {
            for (; ; )
            {
                endText.text = "Next race in: " + timeRemaining;
                yield return new WaitForSeconds(1f);
                timeRemaining--;
                if (timeRemaining == 0)
                {
                    break;
                }
            }
            PlayerPrefs.SetInt("Score", 1);
            SceneManager.LoadScene("HorseRace");
        }
    }
}

