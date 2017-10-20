using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combat_ChatIRC : MonoBehaviour {

    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;
    private bool StartingPhase = true;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject bulletPrefab;
    private string[] CurrentPlayers = new string[10];
    private GameObject[] Players = new GameObject[10];
    private float startTimer = 10f;
    private bool Initialized = false;
    private bool gameStart = false;


    // Use this for initialization
    void Start () {
        IRC = this.GetComponent<TwitchIRC>();
        //IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (startTimer >= 0)
        {
            startTimer -= Time.deltaTime;
        }
        else
        {
            if (Initialized == false)
            {
                InitializePlayers();
                Initialized = true;
                gameStart = true;
                Debug.Log("GAME START");
            }
        }

 
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

        //STARTING PHASE (FINDS PLAYERS)
        if (StartingPhase == true)
        {

            if (CurrentPlayers[CurrentPlayers.Length - 1] == null)
            {

                bool nameCheck = false;
                for (int i = 0; i < CurrentPlayers.Length; i++)
                {
                    if (user == CurrentPlayers[i])
                    {
                        nameCheck = true;
                    }
                }

                if (!nameCheck)
                {
                    for (int i = 0; i < CurrentPlayers.Length; i++)
                    {
                        if (CurrentPlayers[i] == null)
                        {
                            CurrentPlayers[i] = user;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < CurrentPlayers.Length; i++)
            {
                Debug.Log(CurrentPlayers[i]);
            }
        }
        else if (gameStart == true)
        {
            for (int i = 0; i < CurrentPlayers.Length; i++)
            {
                if (user == CurrentPlayers[i])
                {
                    Fire(i, msgString);
                }
            }
        }

      
    
    }

    //INITIALIZE PLAYERS
    void InitializePlayers()
    {
        for (int i = 0; i < CurrentPlayers.Length; i++)
        {
            if (CurrentPlayers[i] != null)
            {
                Players[i] = Instantiate(playerPrefab, new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-4f, 4f)), Quaternion.identity);
            }
        }
        StartingPhase = false;
    }

    void Fire(int username, string direction)
    {
        int rotate;

        if (int.TryParse(direction, out rotate))
        {
            Players[username].transform.Rotate(0, rotate, 0);
            Instantiate(bulletPrefab, Players[username].transform.position, Players[username].transform.rotation);
        }
        else
        {
            IRC.SendMsg("@" + CurrentPlayers[username] + " Enter a number only");
        }

        
    }
}
