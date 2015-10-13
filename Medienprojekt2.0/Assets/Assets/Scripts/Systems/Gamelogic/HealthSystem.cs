using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
    
    //ac = AttributeComponent von Player, bc = BoxCollider von Player, pbc = BoxCollider von Projectile
	AttributeComponent ac;
	BoxCollider2D bc;
	BoxCollider2D pbc;

	// Use this for initialization
	void Start () {
        Animator a = new Animator();
        Animation b = new Animation();
        b.
        ac = this.gameObject.GetComponent<AttributeComponent>();
        bc = this.gameObject.GetComponent<BoxCollider2D>();
		pbc = GameObject.FindWithTag ("Projectile").GetComponent <BoxCollider2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	//Verringert HP des Spielers/Gegners und gibt Todesanzeige. MIN HP = 0
	public void lowerHealth(float damage)
	{
		ac.setHealth (ac.getHealth() - damage);
		if (ac.getHealth() < 0) {
			ac.setHealth(0);
		}
		Debug.Log (ac.getHealth ());
	}

	//Erhöht HP des Spielers/Gegners. MAX HP = 100
	public void raiseHealth(float hp)
	{
		ac.setHealth(ac.getHealth() + hp);
		if (ac.getHealth () > 100)
			ac.setHealth (100);
	}

}