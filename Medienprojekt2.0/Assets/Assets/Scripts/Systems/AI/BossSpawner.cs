using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {

    public float timeToSpawn;
    bool triggered = false;
    bool spawned = false;
    public BossScript boss;
    public GameObject thunder;
	// Use this for initialization
	void Start () {
        thunder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToSpawn <= 0.0f && !spawned)
        {
            SpawnBoss();
        }

        if (triggered && !spawned)
        {
            timeToSpawn -= Time.deltaTime;
        }

        if(spawned)
        {
            StuffWhileSpawned();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && !spawned)
            triggered = true;
    }

    void SpawnBoss()
    {
        boss.Spawn();
        thunder.SetActive(true);
    }

    void StuffWhileSpawned()
    {
        //To-DO:
        //-Stuff while the boss is spawned
        //-some more stuff
        //(optional)-even more stuff
    }

}
