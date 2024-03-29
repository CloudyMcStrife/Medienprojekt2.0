﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthbarHandler : MonoBehaviour {

    //Transform der Lebensleiste
    RectTransform healthTransform;
    //y-Position der Leiste
    float cachedY;
    float maxXPos;
    float minXPos;
    //aktuelle Lebenspunkte
    float currentHealth;
    float maxHealth;
    float newXPos;
    AttributeComponent attComp;
    Image visualHealth;


	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();
        healthTransform = this.GetComponent<RectTransform>();
        cachedY = healthTransform.localPosition.y ;
        maxXPos = healthTransform.localPosition.x;
        minXPos = healthTransform.localPosition.x - healthTransform.rect.width;
        maxHealth = attComp.getHealth();
        currentHealth = maxHealth;
        visualHealth = this.GetComponent<Image>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
        
        

           if (currentHealth != attComp.getHealth())
            {
            currentHealth = attComp.getHealth();
            newXPos = MapValues(currentHealth, 0, maxHealth, minXPos, maxXPos);
            healthTransform.localPosition = new Vector3(newXPos, cachedY);
            }
 

        if(currentHealth > maxHealth/2)
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }

        else
        {
            visualHealth.color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }


    }

    //Methode zum mappen der Position und Lebenspunkte
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
