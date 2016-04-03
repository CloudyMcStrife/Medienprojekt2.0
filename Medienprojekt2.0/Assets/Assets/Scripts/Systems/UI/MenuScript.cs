using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitDialogue;
	Button play;
	Button exit;
	bool playing;
	GameObject quit;
	GameObject pauseMenu;

	// Use this for initialization
	void Start () {
        //Canvas fuer Dialogfenster
		quitDialogue = quitDialogue.GetComponent<Canvas>();
        //Play-Button
		play =  GameObject.Find("Play").GetComponent<Button>();
        //Exit-Button
		exit = GameObject.Find("Exit").GetComponent<Button>();
		quitDialogue.enabled = false;
	}


	public void ExitPress()
	{
		quitDialogue.enabled = true;
		play.enabled = false;
		exit.enabled = false;
	}

	public void NoPress()
	{
		quitDialogue.enabled = false;
		play.enabled = true;
		exit.enabled = true;
	}

	public void PlayPress()
	{
			Application.LoadLevel (1);
	}

	public void ShutdownGame()
	{
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
