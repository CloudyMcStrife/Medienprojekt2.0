using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	GameObject PauseUI;

    public GameObject Canvas;

    public RectTransform rectPause;

	private bool paused = false;

    private float animEnd = 1;

    private float animTime = 0;

    bool animDone = false;

    Vector3 begin;

    Vector3 final;

    Vector3 diff;

	// Use this for initialization
	void Start () {
        PauseUI = GameObject.Find("PauseUI");
        Canvas = GameObject.Find("CanvasPM");
		PauseUI.SetActive (false);
        rectPause = PauseUI.GetComponent<RectTransform>();
        begin = rectPause.localScale;
        final = new Vector3(1.2f, 1.4f, 1);
        diff = final - begin;
    }
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            animDone = false;
        }
			if(paused && !animDone)
			{
                
				PauseUI.SetActive(true);

                if(animTime < animEnd)
                {
                    animTime = Mathf.Clamp(animTime + Time.deltaTime*10, 0, animEnd);
                }
                
                Vector3 actualScale = begin + animTime * diff;

                rectPause.localScale = actualScale;

                if (animTime == animEnd)
                {
                    Time.timeScale = 0;
                    animDone = true;
                }
			}
			else if(!paused && !animDone)
			{
				PauseUI.SetActive(false);
                rectPause.localScale = begin;
                animTime = 0;
                Time.timeScale = 1;
                animDone = true;
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
