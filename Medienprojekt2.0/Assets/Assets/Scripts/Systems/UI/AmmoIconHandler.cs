using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoIconHandler : MonoBehaviour {

    public Text txtS;
    public Text txtB;
    GameObject AmmoPanelPrimary;
    GameObject AmmoPanelSecondary;
    public AttributeComponent attComp;
    bool isPrimary = true;
	// Use this for initialization
	void Start () {
        AmmoPanelPrimary = GameObject.Find("AmmoPanelPrimary");
        AmmoPanelSecondary = GameObject.Find("AmmoPanelSecondary");

        AmmoPanelSecondary.SetActive(false);

        txtS.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();


    }
	
	// Update is called once per frame
	void Update () {
        setLabel();
        
    }

    public void setLabel()
    {
        if (isPrimary)
        {
            txtS.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
        }
        else
        {
            txtB.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
        }
    }    

    public void changeAmmo()
    {
        isPrimary = !isPrimary;
        AmmoPanelPrimary.SetActive(isPrimary);
        AmmoPanelSecondary.SetActive(!isPrimary);
    } 
}
