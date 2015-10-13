using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    AttributeComponent attributes;
    Animator anim;
    bool blocking;

	public Transform meleeCheck;

	// Use this for initialization
	void Awake () {
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
        anim = (Animator)GetComponent(typeof(Animator));
	}
	
	// Update is called once per frame
	void Update () {
        if (attributes.stamina == 0)
            unblock();
	}

    public void setBlockReady()
    {
        blocking = true;
    }

    public void block()
    {
        anim.SetBool("Blocking", true);
    }

    public void unblock()
    {
        blocking = false;
        anim.SetBool("Blocking", false);
    }

	public void punch(bool facingRight)
	{
		Vector2 direction;
		
		if (facingRight)
			direction = new Vector2 (1, 0);
		else
			direction = new Vector2 (-1, 0);
		
		RaycastHit2D[] meleeCollider = Physics2D.BoxCastAll (meleeCheck.position, new Vector2(1, 0.5f), 0, direction, 0.1f);
		
		if (meleeCollider.Length != 0) {
			foreach (RaycastHit2D c in meleeCollider) {
				if (c.collider.gameObject.tag == "Enemy" && this.gameObject.tag == "Player") {
					AttributeComponent enemyac = (AttributeComponent)c.collider.gameObject.GetComponent (typeof(AttributeComponent));
					enemyac.setHealth (enemyac.getHealth() - attributes.getDamage());
					Debug.Log ("Meleehit");
				}
				else if(c.collider.gameObject.tag == "Player" && this.gameObject.tag == "Enemy")
				{
					AttributeComponent enemyac = (AttributeComponent) this.gameObject.GetComponent(typeof(AttributeComponent));
					attributes.setHealth (attributes.getHealth() - enemyac.getDamage ());
					Debug.Log ("EnemyMeleeHit");
				}
			}
		}

	}
}
