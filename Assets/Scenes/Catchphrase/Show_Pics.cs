using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Pics : MonoBehaviour {

    public Material image;
    private Image toChange;
    public Texture[] memes = new Texture[21];


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
        GetComponent<Image>().material.mainTexture = memes[Random.Range(0, 20)];
        GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update () {
        
    }
    

}
