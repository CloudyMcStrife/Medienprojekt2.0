using UnityEngine;
using System.Collections;

public class AreafightEvent : MonoBehaviour {

	GameObject booster;
	GameObject enemy11;
	GameObject enemy12;
	GameObject enemy13;
    GameObject alarm;
	int timer;
	bool triggered = false;
	bool spawned;
	float timeSpawn = 0.0f;

    AlarmSystem alarmSystem;

	// Use this for initialization
	void Start () {
		booster = GameObject.Find ("BoosterEbene6");
		enemy11 = GameObject.Find ("enemy1");
		enemy12 = GameObject.Find ("enemy2");
		enemy13 = GameObject.Find ("enemy3");
		enemy11.SetActive (false);
		enemy12.SetActive (false);
		enemy13.SetActive (false);

        alarm = GameObject.Find("Alarm");
        alarmSystem = alarm.GetComponent<AlarmSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			timeSpawn += Time.deltaTime;
            alarmSystem.alarmActivated = true;
		}
		if (triggered && enemy11 != null) {
			enemy11.SetActive (true);
			booster.SetActive (false);
		}
		if (timeSpawn >= 30 && enemy12 != null) {
			enemy12.SetActive (true);
		}
		if (timeSpawn >= 60 && enemy13 != null) {
			enemy13.SetActive (true);
		}
	
		if (enemy11 == null && enemy12 == null && enemy13 == null) {
			booster.SetActive (true);
            alarmSystem.alarmActivated = false;
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
			triggered = true;
	}
}
