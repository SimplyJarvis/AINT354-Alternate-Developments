using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Combat_ChatIRC : MonoBehaviour {

    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;
    private bool StartingPhase = true;
    [SerializeField]
    private GameObject playerPrefab;
    private string[] CurrentPlayers = new string[10];
    private GameObject[] Players = new GameObject[10];
    [SerializeField]
    private float startTimer = 10f;
    private bool Initialized = false;
    private bool gameStart = false;
    private int PlayerCount = 0;
    [SerializeField]
    GameObject instructions;
    [SerializeField]
    Text countText;
    int count = 0;
    private int totalGames = 0;
    private int maxGames = 2;

    //Handle TimeLine Shit
    [SerializeField]
    private GameObject gameCamera;
    [SerializeField]
    private GameObject attractCamera;
    [SerializeField]
    private GameObject timeLine;

    // Use this for initialization
    void Start () {
        IRC = GameObject.FindGameObjectWithTag("ControllerIRC").GetComponent<TwitchIRC>();
        //IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (startTimer >= 0)
        {
            startTimer -= Time.deltaTime;
        }
       

 
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

        if (msgString == "!start")
        {
            if (count > 1)
            {
                if (startTimer <= 0)
                {
                    StartingPhase = false;
                    startTimer = -5;
                    InitializePlayers();
                    Initialized = true;
                    gameStart = true;
                    Debug.Log("GAME START");
                }
            }
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
                                count++;
                                countText.text = count + "/10 Joined";
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
                    if (Players[i] != null)
                    {
                        Fire(i, msgString);
                    }
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
                Players[i] = Instantiate(playerPrefab, new Vector3(Random.Range(-16.0f, 16.0f), 1.4f, Random.Range(-8.0f, 8.0f)), playerPrefab.transform.rotation);
                Players[i].GetComponentInChildren<TextMesh>().text = CurrentPlayers[i];
                PlayerCount++;
            }
        }
        StartingPhase = false;
        //instructions.SetActive(false);
        countText.text = "";
        attractCamera.SetActive(false);
        gameCamera.SetActive(true);
        timeLine.SetActive(false);

    }
    //FIRE AND MOVE
    void Fire(int username, string direction)
    {
        int rotate;

        if (int.TryParse(direction, out rotate))
        {
            Players[username].transform.Rotate(0, rotate, 0);

            Players[username].GetComponentInChildren<Combat_BulletSpawn>().BulletFire();
        }
        else
        {
            if (direction.Contains("up"))
            {
                Players[username].GetComponent<TankMove>().Move("up");
            }
            else if (direction.Contains("down"))
            {
                Players[username].GetComponent<TankMove>().Move("down");
            }
            else if (direction.Contains("left"))
            {
                Players[username].GetComponent<TankMove>().Move("left");
            }
            else if (direction.Contains("right"))
            {
                Players[username].GetComponent<TankMove>().Move("right");
            }
            else
            {
                IRC.SendMsg("@" + CurrentPlayers[username] + " You may enter a number or up,down,left,right");
            }
        }

        
    }

    public void DeathCount()
    {
        PlayerCount--;
        if (PlayerCount == 1)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("FINISHED");

        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                countText.text = "Finish";
            }
        }
        Invoke("Restart", 8f);
    }

    void Restart()
    {
        if (PlayerPrefs.GetInt("Score") == 1)
        {
            SceneManager.LoadScene("ChooseGame");
        }
        else
        {
            PlayerPrefs.SetInt("Score", 1);
        }
        //SceneManager.LoadScene(1);
    }
}
