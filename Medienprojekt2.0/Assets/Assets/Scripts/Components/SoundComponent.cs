using UnityEngine;
using System.Collections;

public class SoundComponent : MonoBehaviour {

    float ttl; //Zeit für Object bis zur Zerstörung

    AudioSource sound;

	// Use this for initialization
	void Start () {
        sound = this.GetComponent<AudioSource>();
        ttl = sound.clip.length;
	}
	
	// Update is called once per frame
	void Update () {

        ttl -= Time.deltaTime;

       /* if(ttl <= 0)
        {
            Destroy(this.gameObject);
        }
       */
	
	}
}
