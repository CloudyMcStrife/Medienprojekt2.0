using UnityEngine;
using System.Collections;

public class FloorLightTrigger : MonoBehaviour {

    GameObject floorLights;
    float timer;
    bool lightsEnabled;

	// Use this for initialization
	void Start () {
        Transform floorTransform = transform.Find("FloorLights");
        floorLights = floorTransform.gameObject;
        timer = 0.0f;
        lightsEnabled = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (lightsEnabled)
        {
            if(timer < 2.0f)
            {
                int deg = (int)(Mathf.Rad2Deg * timer);
                if ((deg % 2) == 0)
                    floorLights.SetActive(true);
                else
                    floorLights.SetActive(false);
            }
            else
            {
                floorLights.SetActive(true);
            }
            timer += Time.deltaTime;
        }
        else
        {
            floorLights.SetActive(false);
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            lightsEnabled = true;
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Player")
            lightsEnabled = false;
    }
}
