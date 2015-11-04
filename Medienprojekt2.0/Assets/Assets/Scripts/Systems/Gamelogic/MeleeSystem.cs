using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    AttributeComponent attributes;
	CharacterMovement movement;
    Animator anim;
    public bool blocking;
    public bool blockAction;

    public int blockStartCost;
    public float costPerSecond;

	Transform meleeCheck;
	
	public bool firstAnim;
	public bool MeleeAttackInQueue;

	// Use this for initialization
	void Awake () {
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
		movement = (CharacterMovement)GetComponent (typeof(CharacterMovement));
        anim = (Animator)GetComponent(typeof(Animator));
        meleeCheck = this.gameObject.GetComponent<Transform>();
		MeleeAttackInQueue = firstAnim = false;
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

	public void setBlockDone()
	{
		blocking = false;
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
        anim.SetBool("Blocking", false);
		blockAction = false;
    }

	//Called am Ende der Animation
    public void setAnimationDone()
    {
		if (anim.GetBool("MeleeAttackInQueue")) {
				anim.SetTrigger("MeleeAttack");
		}
		else {
			firstAnim = false;
			anim.SetBool ("firstAnim", false);
			anim.SetTrigger("HitDone");
		}
		anim.SetBool ("MeleeAttackInQueue", false);
    }


	//Called durch Input
    public void punch()
	{
		if (anim.GetBool("firstAnim")) {
			anim.SetBool ("MeleeAttackInQueue", true);
			MeleeAttackInQueue = true;
		}
		else {
			firstAnim = true;
			anim.SetBool ("firstAnim", true);
			anim.SetTrigger("MeleeAttack");
		}
	}

	public void applyMeleeDamage()
	{
		Vector2 direction;
		
		if (movement.facingRight)
			direction = new Vector2 (1, 0);
		else
			direction = new Vector2 (-1, 0);
		
		RaycastHit2D[] meleeCollider = Physics2D.BoxCastAll (meleeCheck.position, new Vector2 (.8f, 0.5f), 0, direction);
		/*
		 * Debug Lines to draw the hitzone of the melee hit
		Vector3 debug1 = new Vector3(meleeCheck.position.x, meleeCheck.position.y+0.5f, meleeCheck.position.z);
		Vector3 debug2 = new Vector3 (meleeCheck.position.x, meleeCheck.position.y - 0.5f, meleeCheck.position.z);
		Vector3 debug3 = debug1 + (Vector3)direction*.8f;
		Vector3 debug4 = debug2 + (Vector3)direction*.8f;

		Debug.DrawLine (debug1, debug2,Color.red,5.0f);
		Debug.DrawLine (debug2, debug4,Color.red,5.0f);
		Debug.DrawLine (debug3, debug4,Color.red,5.0f);
		Debug.DrawLine (debug1, debug3,Color.red,5.0f);
		*/
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
