using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	public GameObject PauseUI;

	private bool paused = false;

	// Use this for initialization
	void Start () {
		PauseUI.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")) {
			paused = !paused;
			if(paused)
			{
				PauseUI.SetActive(true);
				Time.timeScale = 0;
			}
			else
			{
				PauseUI.SetActive(false);
				Time.timeScale = 1;
			}

		}
	}

	public void Resume()
	{
		paused = false;
		PauseUI.SetActive (false);
		Time.timeScale = 1;
	}

	public void openMainMenu()
	{
		Application.LoadLevel (0);
        Time.timeScale = 1;
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
