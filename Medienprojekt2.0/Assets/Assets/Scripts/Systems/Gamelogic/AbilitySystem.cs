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
        //Sets cooldown on active
        attComp.setCooldown2Active(true);

        //Create clone from Prefab
        GameObject clone = (GameObject)Instantiate(illusion);
        clone.name = "Klon";
        //Set clone at same position as player
        clone.transform.position = transform.position;
        //Disable collisions between player and clone
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<BoxCollider2D>(), clone.GetComponent<BoxCollider2D>());
        attComp.setTTL();
    }
}
