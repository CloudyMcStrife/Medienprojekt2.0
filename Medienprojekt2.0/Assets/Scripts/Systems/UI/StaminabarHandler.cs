using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminabarHandler : MonoBehaviour {

    RectTransform staminaTransform;
    float cachedY;
    float maxXPos;
    float minXPos;
    float currentStamina;
    float maxStamina;
    float newXPos;
    AttributeComponent attComp;
    Image visualStamina;

	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();
        staminaTransform = this.GetComponent<RectTransform>();
        cachedY = staminaTransform.localPosition.y ;
        maxXPos = staminaTransform.localPosition.x;
        minXPos = staminaTransform.localPosition.x - staminaTransform.rect.width;
        maxStamina = attComp.maxStamina;
        currentStamina = maxStamina;
        visualStamina = this.GetComponent<Image>();
        
	}
	
	// Update is called once per frame
	void Update () {
           if (currentStamina != attComp.stamina)
            {
            currentStamina = attComp.stamina;
            newXPos = MapValues(currentStamina, 0, maxStamina, minXPos, maxXPos);
            staminaTransform.localPosition = new Vector3(newXPos, cachedY);
            }
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
