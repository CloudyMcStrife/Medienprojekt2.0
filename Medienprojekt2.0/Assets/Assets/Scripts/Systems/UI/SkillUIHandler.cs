using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillUIHandler : MonoBehaviour {

    AttributeComponent attComp;
    Text cooldown1Txt;
    GameObject skill1Icon;
    Image skill1IconTrans;

    Text cooldown2Txt;
    GameObject skill2Icon;
    Image skill2IconTrans;

    float cooldown1;
    float cooldown2;


	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();

        cooldown1Txt = GameObject.Find("CooldownTxt").GetComponent<Text>();
		cooldown1Txt.text = "";
        skill1Icon = GameObject.Find("SkillshotIcon");
        skill1IconTrans = GameObject.Find("SkillshotIconTransp").GetComponent<Image>();
        skill1IconTrans.enabled = false;
        cooldown1 = attComp.getCooldown1();

        cooldown2Txt = GameObject.Find("CooldownTxt2").GetComponent<Text>();
        cooldown2Txt.text = "";
        skill2Icon = GameObject.Find("CloneIcon2");
        skill2IconTrans = GameObject.Find("CloneIcon2Transp").GetComponent<Image>();
        skill2IconTrans.enabled = false;
        cooldown2 = attComp.getCooldown2();


    }
	
	// Update is called once per frame
	void Update () {
        if(attComp.getCooldown1Active())
        {
            skill1Icon.SetActive(false);
            skill1IconTrans.enabled = true;
            //skill1Icon.color = new Color32(0, 0, 0,(byte)190);
            cooldown1Txt.text = System.String.Format("{0:0.0}", cooldown1);      // "123.46"
            cooldown1 -= Time.deltaTime;
        }
        

        if (attComp.getCooldown2Active())
        {
            skill2Icon.SetActive(false);
            skill2IconTrans.enabled = true;
            //skill1Icon.color = new Color32(0, 0, 0,(byte)190);
            cooldown2Txt.text = System.String.Format("{0:0.0}", cooldown2);      // "123.46"
            cooldown2 -= Time.deltaTime;
            
        }

    }
    void FixedUpdate()
    {
        if (cooldown2 <= 0.0f)
        {

            attComp.setCooldown2Active(false);
            skill2Icon.SetActive(true);
            skill2IconTrans.enabled = false;
            // skill1Icon.color = new Color32(0, 0, 0, (byte)255);
            cooldown2Txt.text = "";
            cooldown2 = attComp.getCooldown2();
        }

        if (cooldown1 <= 0)
        {
            skill1Icon.SetActive(true);
            skill1IconTrans.enabled = false;
            attComp.setCooldown1Active(false);
            cooldown1 = attComp.getCooldown1();
            // skill1Icon.color = new Color32(0, 0, 0, (byte)255);
            cooldown1Txt.text = "";
        }
    }
}
