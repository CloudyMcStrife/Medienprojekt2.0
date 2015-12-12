using UnityEngine;
using System.Collections;

public class SoundComponent : MonoBehaviour {


    AudioSource sound;

	// Use this for initialization
	void Start () {
        sound = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if(!sound.isPlaying)
        {
            Destroy(this.gameObject);
        }
       
	
	}
}
