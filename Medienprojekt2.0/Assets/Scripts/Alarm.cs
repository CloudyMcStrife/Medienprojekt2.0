using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour {
    
    float degree;

    Light l;

    private int s;
    
	// Use this for initialization
	void Start () {
        degree = -180.0f;
        s = 1;
        l = GetComponent<Light>();
        l.type = LightType.Directional;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime*500);
    }
}
