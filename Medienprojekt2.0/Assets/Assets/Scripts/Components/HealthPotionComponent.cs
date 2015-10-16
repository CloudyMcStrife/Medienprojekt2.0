using UnityEngine;
using System.Collections;

public class HealthPotionComponent : MonoBehaviour {

    float healValue = 25f;
    BoxCollider2D collPotion;
    SpriteRenderer sprite;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        collPotion = (BoxCollider2D)GetComponent(typeof(BoxCollider2D));
        sprite = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.Equals(player))
        {
            Debug.Log("Player collision");
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
