using UnityEngine;
using System.Collections;

public class LightMovementComponent : MonoBehaviour {

    public float maxAxisPosition, minAxisPosition;
    public bool yAxis;
    int axis;
    int scale;
    public float velocity;
    public bool changeColor;

    public bool startRight;

	// Use this for initialization
	void Start () {
        axis = yAxis ? 1 : 0;
        scale = startRight ? 1 : -1;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = transform.position;
        newPos[axis] += scale * velocity * Time.deltaTime;
        transform.position = newPos;

        float axisValue = transform.position[axis];
	    if(axisValue >= maxAxisPosition || axisValue <= minAxisPosition)
        {
            if(axisValue >= maxAxisPosition)
            {
                axisValue = maxAxisPosition;
                scale = -1;
            }
            else
            {
                axisValue = minAxisPosition;
                scale = 1;
            }
        }
	}
}
