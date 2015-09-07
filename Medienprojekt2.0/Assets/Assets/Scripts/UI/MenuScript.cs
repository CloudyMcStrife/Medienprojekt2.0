using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitDialogue;
	public Button playText;
	public Button exitText;
	bool playing;
	GameObject quit;
	GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		quitDialogue = quitDialogue.GetComponent<Canvas>();
		pauseMenu = GameObject.Find ("StartMenu");
		quit = GameObject.Find ("QuitDialogue");
		playText = playText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
		quitDialogue.enabled = false;
		playing = false;
	}


	public void ExitPress()
	{
		if (playing) {
			quit.SetActive(true);
		}
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
		if (playing) {
			pauseMenu.SetActive (false);
			quit.SetActive (false);
		} else {
			Application.LoadLevel (1);
			playing = true;
		}
	}

	public void ShutdownGame()
	{
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
