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
            if(timer < 1.0f)
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

//void OnValidate()
//{
//    if (transform.childCount > 0)
//    {
//        UnityEditor.EditorApplication.delayCall += () =>
//        {
//            DestroyImmediate(transform.GetChild(0).gameObject, true);
//        };
//    }
//
//    float width = GetComponent<BoxCollider2D>().size.x;
//    float height = GetComponent<BoxCollider2D>().size.y;
//
//    Debug.Log(transform.childCount);
//    floorLights = (GameObject)Instantiate(new GameObject());
//    floorLights.transform.parent = (transform);
//
//    Debug.Log(width + " " + height);
//
//    GameObject light = new GameObject("Light", typeof(Light));
//
//    for (int i = 0; i < numberOfLights; i++)
//    {
//        float x = (i + 0.5f) - ((float)numberOfLights / 2.0f);
//        GameObject g = (GameObject)Instantiate(light, new Vector3(x - width * 0.5f, -height * 0.5f), new Quaternion());
//        g.transform.parent = (floorLights.transform);
//    }
//    timer = 0.0f;
//    lightsEnabled = false;
//
//    floorLights.name = "FloorLights";
//    //floorLights.transform.localPosition -= floorLights.transform.position - new Vector3(width * 2, height * 2);
//}