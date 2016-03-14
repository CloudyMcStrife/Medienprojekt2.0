using UnityEngine;
using System.Collections;

public class EbeneNavigationSystem : MonoBehaviour {

	public GameObject screenFader;
	GUITexture fadeTexture;
    GameObject player;
	Transform playerTransform;
	public GameObject targetObject;
	Transform target;
	Transform cam;

	bool isFading = true;

	bool fadeToBlack = false;
	//Setup a default blank texture for fading if none is supplied
	
	// Use this for initialization
	void Start () { 
		fadeTexture = (GUITexture)screenFader.GetComponent (typeof(GUITexture));
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
		target = (Transform)targetObject.GetComponent(typeof(Transform));
		cam = (Transform)GameObject.FindGameObjectWithTag ("MainCamera").GetComponent(typeof(Transform));
		fadeTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		fadeTexture.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeToBlack && isFading)
			FadeToBlack ();
		else if(!fadeToBlack && isFading)
			FadeToClear ();
	}


	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && Input.GetKeyDown ("e") && !isFading) {
				isFading = true;
				fadeToBlack = true;
		}
	}

    //Zum aufklaren des Bildschirms
    //Lerpt zwischen der momentanen Farbe der Textur und "Klar" bis der Alphawert der Textur kleiner als 0.1 ist.
    //Danach wird die Farbe auf "Klar" gesetzt.
	void FadeToClear()
    { 
			fadeTexture.color = Color.Lerp (fadeTexture.color, Color.clear, 1.5f * Time.deltaTime);
			if(fadeTexture.color.a <= 0.1f)
			{
				fadeTexture.color = Color.clear;
				fadeTexture.enabled = false;
				isFading = false;
			}
	}

    //Um den Bildschirm schwarz zu färben
    //Lerpt zwischen der momentanen Farbe der Textur und "Schwarz" bis der Alphawert der Textur größer als 0.8 ist.
    //Danach wird die Farbe auf "Schwarz" gesetzt.
    void FadeToBlack()
	{
		fadeTexture.enabled = true;
		fadeTexture.color = Color.Lerp (fadeTexture.color, Color.black, 1.5f * Time.deltaTime);
		if (fadeTexture.color.a >= 0.8f) {
			fadeTexture.color = Color.black;
			fadeToBlack = false;
			playerTransform.position = target.position;
			cam.position = new Vector3 (target.position.x, target.position.y, cam.position.z);
		}
	}
}
