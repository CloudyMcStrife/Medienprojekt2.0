using UnityEngine;
using System.Collections;

public class ItemHoverComponent : MonoBehaviour
{


    public float xMagnitude;
    float minX, maxX;

    public float yMagnitude;
    float minY, maxY;

    public float xFrequency;
    public float yFrequency;

    float xParameter;
    float yParameter;
    float deltaSum = 0.0f;

    bool pingPong;

    // Use this for initialization
    void Start()
    {
        xParameter = 0.0f;
        yParameter = 0.0f;

        minX = this.transform.position.x - xMagnitude;
        maxX = this.transform.position.x + xMagnitude;

        minY = this.transform.position.y - yMagnitude;
        maxY = this.transform.position.y + yMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        deltaSum += Time.deltaTime;
        if (deltaSum >= (2.0f*3141592.0f))
            deltaSum -= (2.0f*3141592.0f);
        smoothStep(deltaSum);
        Interpolate();
    }

    void Interpolate()
    {
        float currentX = minX + xParameter * (maxX - minX);
        float currentY = minY + yParameter * (maxY - minY);

        this.transform.position = new Vector2(currentX, currentY);
    }

    void smoothStep(float parameter)
    {
        xParameter = 0.5f * (1.0f - Mathf.Cos(Mathf.PI * parameter * xFrequency));
        yParameter = 0.5f * (1.0f - Mathf.Cos(Mathf.PI * parameter * yFrequency));
    }
}