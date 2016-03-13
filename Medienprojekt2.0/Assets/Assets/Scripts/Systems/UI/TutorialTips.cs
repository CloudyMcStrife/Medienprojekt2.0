using UnityEngine;
using System.Collections;

public class TutorialTips : MonoBehaviour {

    //Flag to show the textures
    bool showControls;

    //Helptext to display
    public Texture2D toolTipImg;

    //Offset in X & Y direction for text placement
    public float offsetX;
    public float offsetY;

    //Texture for NPCHead
    public Texture2D TipHead;

    //Offset in X & Y direction for Headtexture placement
    public float offsetXHead;
    public float offsetYHead;


	// Use this for initialization
	void Start () {
        showControls = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        if (showControls)
        {
            showToolTip();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
            showControls = true;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
            showControls = false;
    }

    //Method to activate the Tooltips
    public void showToolTip()
    {
        //Positioning for text and head
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Rect textPos = new Rect(pos.x - toolTipImg.width * 0.5f + offsetX, Camera.main.pixelHeight - pos.y - toolTipImg.height * 2.0f + offsetY, toolTipImg.width, toolTipImg.height);
        Rect headPos = new Rect(pos.x - TipHead.width * 0.5f + offsetXHead, Camera.main.pixelHeight - pos.y - TipHead.height * 2.0f + offsetYHead, TipHead.width, TipHead.height);
        GUI.Label(headPos, TipHead);
        GUI.Label(textPos, toolTipImg);
    }
}
