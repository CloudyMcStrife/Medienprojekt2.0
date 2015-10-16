using UnityEngine;
using System.Collections;

public class HealthPotionComponent : MonoBehaviour {

    float healValue = 25f;
    BoxCollider2D collider;
    SpriteRenderer sprite;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject..name == "Player")
        {
            Debug.Log("Hello");
            HealthSystem hs = player.GetComponent<HealthSystem>();
            AttributeComponent attComp = player.GetComponent<AttributeComponent>();
            if(attComp.getHealth() + healValue <= attComp.getMaxHealth())
            {
                hs.raiseHealth(healValue);
            }
            else
            {
                hs.raiseHealth(attComp.getMaxHealth() - attComp.getHealth());
            }
            
            Destroy(this);
        }
    }
}
