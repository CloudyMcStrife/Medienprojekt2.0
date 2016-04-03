using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour {

    AttributeComponent attComp;
    public GameObject illusion;
	// Use this for initialization
	void Start () {
        attComp = GameObject.Find("Player").GetComponent<AttributeComponent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //Ability to create a clone
    public void clone()
    {
        //Setze cooldown auf aktiv
        attComp.setCooldown2Active(true);

        //Erstelle Klon aus Prefab
        GameObject clone = (GameObject)Instantiate(illusion);
        clone.name = "Klon";
        //Setze Klon auf aktuelle Spielerposition
        clone.transform.position = transform.position;
        //Deaktiviere Kollision zwischen Spieler und Klon
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<BoxCollider2D>(), clone.GetComponent<BoxCollider2D>());
        //Setze Time-To-Live für Klon
        attComp.setTTL();
    }
}
