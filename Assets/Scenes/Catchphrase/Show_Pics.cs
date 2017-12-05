using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Data;
using System;
using Mono.Data.Sqlite;

public class Show_Pics : MonoBehaviour {

    public Material image;
    private Image toChange;
    public Texture[] memes = new Texture[21];
    private int chosenMeme;
    private TwitchIRC IRC;
    private LinkedList<GameObject> messages = new LinkedList<GameObject>();
    public int maxMessages = 100;
    public GameObject[] squares = new GameObject[9];
    private int[] squareRemove = new int[9];
    private int chosenSquare;
    private int timeRemaining = 15;
    public Text timeCounter;
    public Text[] revealArray = new Text[9];
    private string winner;
    public Text currentWinner;
    public Text prevWinners;
    private int totalGames = 0;
    private int maxGames = 5;
    private List<int> ygoNumbers = new List<int>();
    private List<string> ygoNames = new List<string>();
    private int randomNum;
    private int[] YGONumber = new int[8904];
    private string[] YGO = new String[8904];
    private int counter = 0;
    private bool odd = false;
    private int x = 0;
    private string nameCard = "";
    private string lastWinner;
    private string oldWinners;
    private bool onOff = false;

    public TextAsset csv;


    // Used for downloading images

    //IEnumerator Start()
    //{

    //    string[] records = csv.text.Split('\n');
    //    foreach (string record in records)
    //    {

    //        string[] fields = record.Split(';');
    //        foreach (string field in fields)
    //        {
    //            if (Int32.TryParse(field, out x))
    //            {
    //                YGONumber[counter] = Int32.Parse(field);
    //            }
    //            YGO[counter] = field;
    //            if (odd == false)
    //            {
    //                odd = true;
    //            }
    //            else
    //            {
    //                counter++;
    //                odd = false;
    //            }

    //        }
    //    }

    //    randomNum = UnityEngine.Random.Range(0, 8903);

    //    //Debug.Log(YGONumber[0] + YGO[0]);

    //    //string conn = "URI=file:Assets/Scenes/Catchphrase/cards.cdb"; //Path to database.
    //    //IDbConnection dbconn;
    //    //dbconn = (IDbConnection)new SqliteConnection(conn);
    //    //dbconn.Open(); //Open connection to the database.
    //    //IDbCommand dbcmd = dbconn.CreateCommand();
    //    //string sqlQuery = "SELECT id,name FROM texts";
    //    //dbcmd.CommandText = sqlQuery;
    //    //IDataReader reader = dbcmd.ExecuteReader();
    //    //while (reader.Read())
    //    //{
    //    //    ygoNumbers.Add(reader.GetInt32(0));
    //    //    ygoNames.Add(reader.GetString(1));


    //    //}
    //    //randomNum = UnityEngine.Random.Range(0, ygoNumbers.Count);
    //    //Debug.Log("value=" + ygoNumbers[randomNum] + "  name =" + ygoNames[randomNum]);
    //    //reader.Close();
    //    //reader = null;
    //    //dbcmd.Dispose();
    //    //dbcmd = null;
    //    //dbconn.Close();
    //    //dbconn = null;

    //    // Debug.Log(ygoNumbers.Count);

    //    Debug.Log("value=" + YGONumber[randomNum] + "  name =" + YGO[randomNum]);
    //    nameCard = YGO[randomNum].Substring(0, YGO[randomNum].Length - 1);

    //    string url = "https://raw.githubusercontent.com/moecube/ygopro-images/master/pics/" + YGONumber[randomNum] + ".jpg";

    //    Texture2D tex;
    //    tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
    //    WWW www = new WWW(url);
    //    yield return www;
    //    www.LoadImageIntoTexture(tex);
    //    GetComponent<Image>().material.mainTexture = tex;
    //    GetComponent<Image>().enabled = false;
    //    GetComponent<Image>().enabled = true;

    //    StartCoroutine("RemoveSquare");
    //    IRC = GameObject.Find("Twitch").GetComponent<TwitchIRC>();
    //    IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);


    //}

    void Start()
    {   
        chosenMeme = UnityEngine.Random.Range(0, 20);
        GetComponent<Image>().material.mainTexture = memes[chosenMeme];
        GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = true;


        StartCoroutine("RemoveSquare");
        IRC = GameObject.Find("Twitch").GetComponent<TwitchIRC>();
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
    }

    // Update is called once per frame
    void Update () {
        if (totalGames == maxGames)
        {
            timeCounter.text = "Returning to Main Menu in: " + timeRemaining.ToString();
        }
        else
        {
            timeCounter.text = "Time remaining until next reveal: " + timeRemaining.ToString();
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
        //Polling to delete
        if (msgString.ToLower() == "a" && squareRemove[0] != -1)
        {
            squareRemove[0]++;
            revealArray[0].text = "Votes for revealing A: " + squareRemove[0].ToString();
        }
        else if (msgString.ToLower() == "b" && squareRemove[1] != -1)
        {
            squareRemove[1]++;
            revealArray[1].text = "Votes for revealing B: " + squareRemove[1].ToString();
        }
        else if (msgString.ToLower() == "c" && squareRemove[2] != -1)
        {
            squareRemove[2]++;
            revealArray[2].text = "Votes for revealing C: " + squareRemove[2].ToString();
        }
        else if (msgString.ToLower() == "d" && squareRemove[3] != -1)
        {
            squareRemove[3]++;
            revealArray[3].text = "Votes for revealing D: " + squareRemove[3].ToString();
        }
        else if (msgString.ToLower() == "e" && squareRemove[4] != -1)
        {
            squareRemove[4]++;
            revealArray[4].text = "Votes for revealing E: " + squareRemove[4].ToString();
        }
        else if (msgString.ToLower() == "f" && squareRemove[5] != -1)
        {
            squareRemove[5]++;
            revealArray[5].text = "Votes for revealing F: " + squareRemove[5].ToString();
        }
        else if (msgString.ToLower() == "g" && squareRemove[6] != -1)
        {
            squareRemove[6]++;
            revealArray[6].text = "Votes for revealing G: " + squareRemove[6].ToString();
        }
        else if (msgString.ToLower() == "h" && squareRemove[7] != -1)
        {
            squareRemove[7]++;
            revealArray[7].text = "Votes for revealing H: " + squareRemove[7].ToString();
        }
        else if (msgString.ToLower() == "i" && squareRemove[8] != -1)
        {
            squareRemove[8]++;
            revealArray[8].text = "Votes for revealing I: " + squareRemove[8].ToString();
        }
        //Correct answer
        if (msgString.ToLower().Contains(memes[chosenMeme].name.ToLower()))
        //if (msgString.ToLower().Contains(nameCard.ToLower()))
        {
            if (onOff == false)
            {
                onOff = true;
                for (int i = 0; i < 9; i++)
                {
                    squares[i].SetActive(false);
                }
                winner = user;
                StopCoroutine("Winner");
                oldWinners = prevWinners.text;
                Debug.Log("Old " + oldWinners);
                prevWinners.text = lastWinner + "\n" + "\n" + oldWinners;
                Debug.Log("Prev " + prevWinners.text);
                lastWinner = winner;
                currentWinner.text = winner + "\n" + "Game name: " + memes[chosenMeme].name;
                StartCoroutine("Winner");                
            }
        }
    }

    IEnumerator RemoveSquare()
    {
        Debug.Log(memes[chosenMeme].name);
        //start wait
        for (;;)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            if (timeRemaining == 0)
            {
                break;
            }            
        }
        timeRemaining = 15;
        //removing square
        for (;;)
        {
            List<int> tooRemove = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (squareRemove[i] == squareRemove.Max())
                {
                    tooRemove.Add(i);
                }
            }
            chosenSquare = tooRemove[UnityEngine.Random.Range(0, tooRemove.Count)];
            squares[chosenSquare].SetActive(false);
            for (int i = 0; i < 9; i++)
            {
                if (squareRemove[i] != -1)
                {
                    squareRemove[i] = 0;
                }
            }
            squareRemove[chosenSquare] = -1;            
            for (int i = 0; i < 9; i++)
            {
                if (squareRemove[i] == -1)
                {
                    revealArray[i].text = "Square revealed";
                }
                else
                {
                    if (i == 0)
                    {
                        revealArray[i].text = "Votes for revealing A: 0";
                    }
                    if (i == 1)
                    {
                        revealArray[i].text = "Votes for revealing B: 0";
                    }
                    if (i == 2)
                    {
                        revealArray[i].text = "Votes for revealing C: 0";
                    }
                    if (i == 3)
                    {
                        revealArray[i].text = "Votes for revealing D: 0";
                    }
                    if (i == 4)
                    {
                        revealArray[i].text = "Votes for revealing E: 0";
                    }
                    if (i == 5)
                    {
                        revealArray[i].text = "Votes for revealing F: 0";
                    }
                    if (i == 6)
                    {
                        revealArray[i].text = "Votes for revealing G: 0";
                    }
                    if (i == 7)
                    {
                        revealArray[i].text = "Votes for revealing H: 0";
                    }
                    if (i == 8)
                    {
                        revealArray[i].text = "Votes for revealing I: 0";
                    }
                }
            }
            //wait for next remove
            for (;;)
            {
                yield return new WaitForSeconds(1f);
                timeRemaining--;
                if (timeRemaining == 0)
                {
                    break;
                }
            }
            timeRemaining = 15;
        }

    }
    IEnumerator Winner()
    {
        //Show winner
        StopCoroutine("RemoveSquare");
        for (int i = 0; i < 9; i++)
        {
            revealArray[i].enabled = false;
        }        
        //Waiting for next round
        timeRemaining = 10;
        totalGames++;
        for (;;)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            if (timeRemaining == 0)
            {
                break;
            }
        }

        
        if (totalGames == maxGames)
        {
            SceneManager.LoadScene("ChooseGame");
        }

        //Start next round
        chosenMeme = UnityEngine.Random.Range(0, 20);
        GetComponent<Image>().material.mainTexture = memes[chosenMeme];
        //randomNum = UnityEngine.Random.Range(0, 8903);
        //string url = "https://raw.githubusercontent.com/moecube/ygopro-images/master/pics/" + YGONumber[randomNum] + ".jpg";
        //nameCard = YGO[randomNum].Substring(0, YGO[randomNum].Length - 1);
        //Texture2D tex;
        //tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        //WWW www = new WWW(url);
        //yield return www;
        //www.LoadImageIntoTexture(tex);
        //GetComponent<Image>().material.mainTexture = tex;
        GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = true;

        
        IRC = GameObject.Find("Twitch").GetComponent<TwitchIRC>();
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
        //Debug.Log("value=" + YGONumber[randomNum] + "  name =" + YGO[randomNum]);

        GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = true;
        timeRemaining = 15;
        for (int i = 0; i < 9; i++)
        {
            revealArray[i].enabled = true;
            squares[i].SetActive(true);
            squareRemove[i] = 0;
        }
        revealArray[0].text = "Votes for revealing A: 0";
        revealArray[1].text = "Votes for revealing B: 0";
        revealArray[2].text = "Votes for revealing C: 0";
        revealArray[3].text = "Votes for revealing D: 0";
        revealArray[4].text = "Votes for revealing E: 0";
        revealArray[5].text = "Votes for revealing F: 0";
        revealArray[6].text = "Votes for revealing G: 0";
        revealArray[7].text = "Votes for revealing H: 0";
        revealArray[8].text = "Votes for revealing I: 0";
        onOff = false;
        StartCoroutine("RemoveSquare");
    }

}
