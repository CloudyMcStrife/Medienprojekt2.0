using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillUIHandler : MonoBehaviour {

    AttributeComponent attComp;
    Text cooldown1Txt;
    GameObject skill1Icon;
    GameObject skill1IconTrans;
    float cooldown1;


	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();
        cooldown1Txt = this.GetComponentInChildren<Text>();
		cooldown1Txt.text = "";
        skill1Icon = GameObject.Find("SkillshotIcon");
        skill1IconTrans = GameObject.Find("SkillshotIconTransp");
        skill1IconTrans.SetActive(false);
        cooldown1 = attComp.getCooldown1();
	}
	
	// Update is called once per frame
	void Update () {
        if(attComp.getCooldown1Active())
        {
            skill1Icon.SetActive(false);
            skill1IconTrans.SetActive(true);
            //skill1Icon.color = new Color32(0, 0, 0,(byte)190);
            cooldown1Txt.text = System.String.Format("{0:0.0}", cooldown1);      // "123.46"
            cooldown1 -= Time.deltaTime;
        }
        if(cooldown1 <= 0)
        {
            skill1Icon.SetActive(true);
            skill1IconTrans.SetActive(false);
            attComp.setCooldown1Active(false);
            cooldown1 = attComp.getCooldown1();
           // skill1Icon.color = new Color32(0, 0, 0, (byte)255);
            cooldown1Txt.text = "";
        }
        
	}
}
