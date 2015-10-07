using UnityEngine;
using System.Collections;

public class DeathUI : MonoBehaviour {

    public GameObject deathUI;
    public AttributeComponent attComp;

	// Use this for initialization
	void Start () {
        deathUI.SetActive(false);
	}

    // Update is called once per frame
    void Update() {
        if (attComp.getHealth() <= 0)
        {
            deathUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void retry()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
        
    }

    public void openMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }


}

