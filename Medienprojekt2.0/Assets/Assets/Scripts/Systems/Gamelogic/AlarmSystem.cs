using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour {

    public Vector3 position;
    public Light l;
    Alarm alarm;
    public AudioClip clip;
    AudioSource audioSource;

    public float waitTillNextAudio;
    float waitingTime;

    public bool alarmActivated;

	// Use this for initialization
	void Start () {
        transform.position = position;
        l = GetComponent<Light>();
        l.type = LightType.Directional;
        l.color = Color.red;
        alarm = l.gameObject.AddComponent<Alarm>();
        l.enabled = false;
        alarmActivated = false;
        waitingTime = 1.0f;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
	}
	
	// Update is called once per frame
	void Update () {
        l.enabled = alarmActivated;  
        if (alarmActivated)
        {
            if (!audioSource.isPlaying)
            {
                if (waitingTime > 0)
                {
                    waitingTime -= Time.deltaTime;
                }
                else
                {
                    audioSource.Play();
                }
            }
            else
            {
                waitingTime = waitTillNextAudio;
            }
        }
        else
        {
            waitingTime = 1.0f;
        }
	}
}
