using UnityEngine;
using System.Collections;

public class HealthPotionComponent : MonoBehaviour {

    float healValue = 25f;
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject player = GameObject.Find("Player");
        if (coll.gameObject.Equals(player))
        {
            HealthSystem hs = player.GetComponent<HealthSystem>();
            AttributeComponent attComp = player.GetComponent<AttributeComponent>();

            if(attComp.getHealth() + healValue <= attComp.getMaxHealth())
            {
                hs.raiseHealth(healValue);
            }
            else if(attComp.getHealth() == attComp.getMaxHealth())
            {
                return;
            }
            else
            {
                hs.raiseHealth(attComp.getMaxHealth() - attComp.getHealth());
            }
            
            Destroy(this.gameObject);
        }
    }
}
