using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitDialogue;
	Button playText;
	Button exitText;
	bool playing;
	GameObject quit;
	GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		quitDialogue = quitDialogue.GetComponent<Canvas>();
		playText =  playText.GetComponent<Button>();
		exitText =  exitText.GetComponent<Button>();
		quitDialogue.enabled = false;
	}


	public void ExitPress()
	{
		quitDialogue.enabled = true;
		playText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress()
	{
		quitDialogue.enabled = false;
		playText.enabled = true;
		exitText.enabled = true;
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
