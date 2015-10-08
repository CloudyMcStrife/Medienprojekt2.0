using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillUIHandler : MonoBehaviour {

    public AttributeComponent attComp;
    Text cooldown1Txt;
    Image skill1Icon;
    float cooldown1;


	// Use this for initialization
	void Start () {
        cooldown1Txt = this.GetComponentInChildren<Text>();
        skill1Icon = this.GetComponentInChildren<Image>();
        cooldown1 = attComp.getCooldown1();
	}
	
	// Update is called once per frame
	void Update () {
        if(attComp.getCooldown1Active())
        {
            skill1Icon.color = new Color32(0, 0, 0,(byte) 0.5);
            cooldown1Txt.text = cooldown1.ToString();
            cooldown1 -= Time.deltaTime;
        }
        if(cooldown1 == 0)
        {
            attComp.setCooldown1Active(false);
            cooldown1 = attComp.getCooldown1();
            skill1Icon.color = new Color32(0, 0, 0, (byte)1.0);
        }
        
	}
}
