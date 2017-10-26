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
        if (msgString.ToLower() == "a")
        {
            squareRemove[0]++;
        }
        else if (msgString.ToLower() == "b")
        {
            squareRemove[1]++;
        }
        else if (msgString.ToLower() == "c")
        {
            squareRemove[2]++;
        }
        else if (msgString.ToLower() == "d")
        {
            squareRemove[3]++;
        }
        else if (msgString.ToLower() == "d")
        {
            squareRemove[4]++;
        }
        else if (msgString.ToLower() == "f")
        {
            squareRemove[5]++;
        }
        else if (msgString.ToLower() == "g")
        {
            squareRemove[6]++;
        }
        else if (msgString.ToLower() == "h")
        {
            squareRemove[7]++;
        }
        else if (msgString.ToLower() == "i")
        {
            squareRemove[8]++;
        }
        //Correct answer
        if (msgString.ToLower().Contains(memes[chosenMeme].name.ToLower()))
        {
            for (int i = 0; i < 9; i++)
            {
                squares[i].SetActive(false);
            }
            Debug.Log("Winner!");
        }
    }

    IEnumerator RemoveSquare()
    {
        Debug.Log(memes[chosenMeme].name);
        yield return new WaitForSeconds(20f);
        for (;;)
        {
            List<int> tooRemove = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                Debug.Log(i);
                if (squareRemove[i] == squareRemove.Max())
                {
                    tooRemove.Add(i);
                }
            }
            chosenSquare = tooRemove[Random.Range(0, tooRemove.Count)];
            squares[chosenSquare].SetActive(false);
            yield return new WaitForSeconds(20f);
        }

    }

}
