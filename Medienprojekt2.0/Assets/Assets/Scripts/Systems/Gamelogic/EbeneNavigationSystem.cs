using UnityEngine;
using System.Collections;

public class EbeneNavigationSystem : MonoBehaviour {

	BoxCollider2D playercoll;
	BoxCollider2D telecoll;
	public GameObject screenFader;
	GUITexture fadeTexture;
    GameObject player;
	Transform playerTransform;
	public GameObject targetObject;
	Transform target;
	Transform cam;
	int timer = 3;

	float fadingTime = 2.0f;
	float currentFade = 0.0f;
	bool fade = true;
	bool black = false;
	bool isFading = true;

	bool fadeToBlack = false;
	//Setup a default blank texture for fading if none is supplied
	
	// Use this for initialization
	void Start () { 
		fadeTexture = (GUITexture)screenFader.GetComponent (typeof(GUITexture));
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playercoll = player.GetComponent<BoxCollider2D>();
            playerTransform = player.transform;
        }
		telecoll = (BoxCollider2D)this.GetComponent (typeof(BoxCollider2D));
		target = (Transform)targetObject.GetComponent(typeof(Transform));
		cam = (Transform)GameObject.FindGameObjectWithTag ("MainCamera").GetComponent(typeof(Transform));
		fadeTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		fadeTexture.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		/*if(fade)
			FadeToClear ();
		if (black)
			FadeToBlack ();
*/
		if (fadeToBlack && isFading)
			FadeToBlack ();
		else if(!fadeToBlack && isFading)
			FadeToClear ();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && Input.GetKeyDown ("t") && !isFading) {
				black = true;
				isFading = true;
				fadeToBlack = true;
		}
	}

	void FadeToClear()
	{
			fadeTexture.color = Color.Lerp (fadeTexture.color, Color.clear, 1.5f * Time.deltaTime);
			if(fadeTexture.color.a <= 0.1f)
			{
				fadeTexture.color = Color.clear;
				fadeTexture.enabled = false;
				fade = false;
				isFading = false;
			}
	}

	void FadeToBlack()
	{
		fadeTexture.enabled = true;
		fadeTexture.color = Color.Lerp (fadeTexture.color, Color.black, 1.5f * Time.deltaTime);
		if (fadeTexture.color.a >= 0.8f) {
			fadeTexture.color = Color.black;
			fadeToBlack = false;
			black = false;
			fade = true;
			playerTransform.position = target.position;
			cam.position = new Vector3 (target.position.x, target.position.y, cam.position.z);
		}
	}
}
