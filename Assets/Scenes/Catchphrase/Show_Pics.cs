using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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


    // Used for downloading images
    //string url = "https://raw.githubusercontent.com/moecube/ygopro-images/master/pics/10000020.jpg";        
    //IEnumerator Start () {
    //    Texture2D tex;
    //    tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
    //    WWW www = new WWW(url);
    //    yield return www;
    //    www.LoadImageIntoTexture(tex);
    //    GetComponent<Image>().material.mainTexture = tex;
    //    GetComponent<Image>().enabled = false;
    //    GetComponent<Image>().enabled = true;
    //}

    void Start()
    {
        chosenMeme = Random.Range(0, 20);
        GetComponent<Image>().material.mainTexture = memes[chosenMeme];
        GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = true;
        StartCoroutine("RemoveSquare");
        IRC = GameObject.Find("Twitch").GetComponent<TwitchIRC>();
        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
    }

    // Update is called once per frame
    void Update () {
        timeCounter.text = "Time remaining until next reveal: " + timeRemaining.ToString();        
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
        {
            for (int i = 0; i < 9; i++)
            {
                squares[i].SetActive(false);
            }
            winner = user;
            StartCoroutine("Winner");
        }
    }

    IEnumerator RemoveSquare()
    {
        Debug.Log(memes[chosenMeme].name);
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
            chosenSquare = tooRemove[Random.Range(0, tooRemove.Count)];
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
        StopCoroutine("RemoveSquare");
        for (int i = 0; i < 9; i++)
        {
            revealArray[i].enabled = false;
        }
        prevWinners.text = currentWinner.text + "\n" + "\n" + prevWinners.text;
        currentWinner.text = winner;
        timeRemaining = 10;
        for (;;)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            if (timeRemaining == 0)
            {
                break;
            }
        }
        chosenMeme = Random.Range(0, 20);
        GetComponent<Image>().material.mainTexture = memes[chosenMeme];
        GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = true;
        timeRemaining = 15;
        for (int i = 0; i < 9; i++)
        {
            revealArray[i].enabled = true;
            squares[i].SetActive(true);
            squareRemove[i] = 0;
        }
        StartCoroutine("RemoveSquare");
        revealArray[0].text = "Votes for revealing A: 0";
        revealArray[1].text = "Votes for revealing B: 0";
        revealArray[2].text = "Votes for revealing C: 0";
        revealArray[3].text = "Votes for revealing D: 0";
        revealArray[4].text = "Votes for revealing E: 0";
        revealArray[5].text = "Votes for revealing F: 0";
        revealArray[6].text = "Votes for revealing G: 0";
        revealArray[7].text = "Votes for revealing H: 0";
        revealArray[8].text = "Votes for revealing I: 0";
    }

}
