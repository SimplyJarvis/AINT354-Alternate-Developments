using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Pics : MonoBehaviour {

    public Material image;
    private Image toChange;
    string url = "https://raw.githubusercontent.com/moecube/ygopro-images/master/pics/10000010.jpg";

    // Use this for initialization
    IEnumerator Start () {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        WWW www = new WWW(url);
        yield return www;
        //Sprite sprites = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        //toChange = image.GetComponent<Image>();
        //toChange.sprite = sprites;
        www.LoadImageIntoTexture(tex);
        GetComponent<Renderer>().material.mainTexture = tex;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    

}
