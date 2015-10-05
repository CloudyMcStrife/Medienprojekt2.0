using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoIconHandler : MonoBehaviour {

    Text txt;
    public AttributeComponent attComp;

	// Use this for initialization
	void Start () {
        txt = this.GetComponent<Text>();
        setLabel();
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setLabel()
    {
        txt.text = attComp.getAmmo() + " / " + attComp.getAmmoCap();
    }    
}
