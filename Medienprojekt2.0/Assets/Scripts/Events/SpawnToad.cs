using UnityEngine;
using System.Collections;

public class SpawnToad : MonoBehaviour {

    SpriteRenderer sprite;
    Rigidbody2D rigid;
    public Texture2D textureToDisplay;
    bool showText = false;
    float timer = 4.0f;
    Transform pos;

    // Use this for initialization
    void Start () {
        pos = GameObject.Find("Toad").GetComponent<Transform>();
        sprite = GameObject.Find("Toad").GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        rigid = GameObject.Find("Toad").GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
            timer = timer - Time.deltaTime;

        if (timer <= 0)
        {
            showText = true;
        }
	}

    void fixedUpdate()
    {
        
    }

    public void spawn()
    {
        sprite.enabled = true;
        rigid.gravityScale = 1;
       
        


    }

    void OnGUI()
    {
        Vector3 p = Camera.main.WorldToScreenPoint(pos.position);
        if(showText)
            GUI.Label(new Rect(p.x - textureToDisplay.width*0.5f, Camera.main.pixelHeight - p.y - textureToDisplay.height*2.0f, textureToDisplay.width, textureToDisplay.height),
            textureToDisplay);
    }
}
