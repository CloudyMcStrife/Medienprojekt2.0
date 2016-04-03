using UnityEngine;
using System.Collections;

public class ItemHoverComponent : MonoBehaviour
{

    //Auslenkung der xPosition
    public float xMagnitude;
    //xKoordinaten an den maximalen Auslenkungspunkten
    float minX, maxX;


    //s.o. nur für Y
    public float yMagnitude;
    float minY, maxY;

    //Geschwindigkeit in der zwischen den Positionen interpoliert werden soll
    public float xFrequency;
    public float yFrequency;


    //Aktueller Wert im Intervall [0..1] nachdem die SmoothStep Funktion ausgeführt wurde
    float xParameter;
    float yParameter;

    //Summe der Zeit um fortlaufende Kurve zu ermöglichen
    float deltaSum = 0.0f;

    // Use this for initialization
    void Start()
    {
        xParameter = 0.0f;
        yParameter = 0.0f;


        //Berechnung der zwei Positionen der maximalen Auslenkung
        minX = this.transform.position.x - xMagnitude;
        maxX = this.transform.position.x + xMagnitude;

        minY = this.transform.position.y - yMagnitude;
        maxY = this.transform.position.y + yMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //Fortlaufende Summe um fortlaufende Sinuskurve zu behalten
        deltaSum += Time.deltaTime;

        //Bei 2 PI, kann 2 PI abgezogen werden, da sich Winkelfunktionen ab dort wiederholen
        if (deltaSum >= (2.0f*3141592.0f))
            deltaSum -= (2.0f*3141592.0f);
        smoothStep(deltaSum);
        Interpolate();
    }

    //Letztendliche Berechnung der Position
    void Interpolate()
    {
        float currentX = minX + xParameter * (maxX - minX);
        float currentY = minY + yParameter * (maxY - minY);

        this.transform.position = new Vector2(currentX, currentY);
    }

    //Smoothstep Funktion die einen Wert der zwischen 0 und 1 oszilliert "glättet" um eine gleichmäßige Kurve zu erzeugen
    void smoothStep(float parameter)
    {
        xParameter = 0.5f * (1.0f - Mathf.Cos(Mathf.PI * parameter * xFrequency));
        yParameter = 0.5f * (1.0f - Mathf.Cos(Mathf.PI * parameter * yFrequency));
    }
}