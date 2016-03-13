using UnityEngine;
using System.Collections;

public class AreafightEvent : MonoBehaviour {

    GameObject booster;

    public GameObject enemyPrefab;
    public int level;

    GameObject enemy11;
    GameObject enemy12;
    GameObject enemy13;

    int timer;
	bool triggered = false;
	bool spawned;
	float timeSpawn = 0.0f;

    AlarmSystem alarmSystem;

	// Use this for initialization
	void Start () {
		booster = GameObject.Find ("BoosterEbene6");

        Vector3 spawnPosition = GameObject.Find("DoorEbene" + level).transform.position;
		enemy11 = (GameObject)Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0,0,0));
		enemy12 = (GameObject)Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
        enemy13 = (GameObject)Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
		enemy11.SetActive (false);
		enemy12.SetActive (false);
		enemy13.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {
        if (triggered)
        {
            timeSpawn += Time.deltaTime;
            alarmSystem.alarmActivated = true;

            if (enemy11 != null)
            {
                enemy11.SetActive(true);
                booster.SetActive(false);
            }
            if (timeSpawn >= 30 && enemy12 != null)
            {
                enemy12.SetActive(true);
            }
            if (timeSpawn >= 60 && enemy13 != null)
            {
                enemy13.SetActive(true);
            }

            if (enemy11 == null && enemy12 == null && enemy13 == null)
            {
                booster.SetActive(true);
                if (alarmSystem != null)
                    alarmSystem.alarmActivated = false;

                //Hier sollten wir dieses Gameobject abschalten, da es nicht mehr gebraucht wird (Oder einer neuen Etage zuweisen?)
                this.enabled = false;
            }
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.name == "Player")
        {
            GameObject alarm = GameObject.Find("Alarm");
            
            alarmSystem = alarm.GetComponent<AlarmSystem>();
            triggered = true;
            
        }
	}
}
