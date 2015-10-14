using UnityEngine;
using System.Collections;

public class DeathUI : MonoBehaviour {

    public GameObject deathUI;
    AttributeComponent attComp;


	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();
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
        
        Application.LoadLevel(1);
        Time.timeScale = 1;

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

