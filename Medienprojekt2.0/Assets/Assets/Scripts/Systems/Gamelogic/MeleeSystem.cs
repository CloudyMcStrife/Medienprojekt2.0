using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    AttributeComponent attributes;
    Animator anim;
    public bool blocking;
    public bool blockAction;

    public int blockStartCost;
    public float costPerSecond;

	public Transform meleeCheck;

	// Use this for initialization
	void Awake () {
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
        anim = (Animator)GetComponent(typeof(Animator));
	}
	
	// Update is called once per frame
	void Update () {
        if (attributes.stamina == 0 && blocking)
            unblock();
        if(blocking)
        {
            attributes.reduceStamina(costPerSecond * Time.deltaTime);
        }
	}

    public void setBlockReady()
    {
        blocking = true;
    }

    public void block()
    {
        if (attributes.stamina > blockStartCost)
        {
            blockAction = true;
            anim.SetBool("Blocking", true);
            attributes.reduceStamina(blockStartCost);
        }
    }

    public void unblock()
    {
        blocking = false;
        blockAction = false;
        anim.SetBool("Blocking", false);
    }

	public void punch(bool facingRight)
	{
		Vector2 direction;
        anim.SetTrigger("MeleeAttack");
		
		if (facingRight)
			direction = new Vector2 (1, 0);
		else
			direction = new Vector2 (-1, 0);
		
		RaycastHit2D[] meleeCollider = Physics2D.BoxCastAll (meleeCheck.position, new Vector2(1, 0.5f), 0, direction, 0.1f);
		
		if (meleeCollider.Length != 0) {
			foreach (RaycastHit2D c in meleeCollider) {
				if (c.collider.gameObject.tag == "Enemy" && this.gameObject.tag == "Player") {
					HealthSystem enemyHealth = (HealthSystem)c.collider.gameObject.GetComponent (typeof(HealthSystem));
					enemyHealth.lowerHealth (attributes.getDamage());
					Debug.Log ("Meleehit");
				}
				else if(c.collider.gameObject.tag == "Player" && this.gameObject.tag == "Enemy")
				{
                    HealthSystem playerHealth = (HealthSystem)c.collider.gameObject.GetComponent(typeof(HealthSystem));
                    playerHealth.lowerHealth(attributes.getDamage());
					Debug.Log ("EnemyMeleeHit");
				}
			}
		}

	}
}
