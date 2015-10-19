using UnityEngine;
using System.Collections;

public class AmmunitionComponent : MonoBehaviour {

    int ammo = 5;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject player = GameObject.Find("Player");
        AttributeComponent attComp = player.GetComponent<AttributeComponent>();

        if (coll.gameObject.Equals(player))
        {
            if (attComp.getAmmo() + ammo <= attComp.getAmmoCap())
            {
                player.GetComponent<AttributeComponent>().increaseAmmo(ammo); 
            }

            else if(attComp.getAmmo() == attComp.getAmmoCap())
            {
                return;
            }
            else
            {
                attComp.increaseAmmo(attComp.getAmmoCap() - attComp.getAmmo());
            }

            Destroy(this.gameObject);
        }
    }
}
