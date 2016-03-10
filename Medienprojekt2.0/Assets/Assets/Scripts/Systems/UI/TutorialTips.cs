using UnityEngine;
using System.Collections;

public class TutorialTips : MonoBehaviour {

    bool showControls;
    public string text;
    public Vector2 boxSize;

    Vector3 labelPos;
	// Use this for initialization
	void Start () {
        bool showControls = false;
        

        
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        if (showControls)
        {
            labelPos = Camera.main.WorldToScreenPoint(transform.position);
            showMoveTip();
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

    public void showMoveTip()
    {
        Rect wichsMich = new Rect(labelPos.x - 75, Camera.main.pixelHeight - labelPos.y - 100, boxSize.x, boxSize.y);
        GUI.Box(wichsMich, text);
    }
}
