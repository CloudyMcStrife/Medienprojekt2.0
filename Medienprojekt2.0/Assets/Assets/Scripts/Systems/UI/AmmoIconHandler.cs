using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoIconHandler : MonoBehaviour {

    public Text txtS;
    public Text txtB;
    GameObject AmmoPanelPrimary;
    GameObject AmmoPanelSecondary;
    AttributeComponent attComp;
    bool isPrimary = true;
    Projectile projectile;
	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();
        AmmoPanelPrimary = GameObject.Find("AmmoPanelPrimary");
        AmmoPanelSecondary = GameObject.Find("AmmoPanelSecondary");

        AmmoPanelSecondary.SetActive(false);

        txtS.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
        txtB.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
    }
	
	// Update is called once per frame
	void Update () {
        setLabel();
        
    }

    public void setLabel()
    {
        if(txtS!=null)
            txtS.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
        if(txtB!=null)
            txtB.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
    }    

    public void changeAmmo()
    {
        isPrimary = !isPrimary;
        AmmoPanelPrimary.SetActive(isPrimary);
        AmmoPanelSecondary.SetActive(!isPrimary);
    }
}
