using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    AttributeComponent attributes;
	CharacterMovement movement;
    Rigidbody2D rigidBody;
    public Animator anim;
    public bool blocking;
    public bool animationRunning;

    public int blockStartCost;
    public float costPerSecond;

	Transform meleeCheck;
    bool idleStateExecuted;

	// Use this for initialization
	void Awake () {
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
		movement = (CharacterMovement)GetComponent (typeof(CharacterMovement));
        rigidBody = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        anim = (Animator)GetComponent(typeof(Animator));
        meleeCheck = this.gameObject.GetComponent<Transform>();
        idleStateExecuted = false;
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
            animationRunning = true;
            anim.SetBool("Blocking", true);
            attributes.reduceStamina(blockStartCost);
        }
    }

    public void unblock()
    {
        anim.SetBool("Blocking", false);
    }

    public void idleState()
    {
        if(!idleStateExecuted)
        {
            anim.SetBool("firstAnim", false);
            anim.SetBool("MeleeAttackInQueue", false);
            anim.ResetTrigger("HitDone");
            anim.ResetTrigger("MeleeAttack");
            animationRunning = false;
            idleStateExecuted = true;
        }
    }



	//Called am Ende der Animation
    public void setAnimationDone()
    {
		if (anim.GetBool("MeleeAttackInQueue")) {
				anim.SetTrigger("MeleeAttack");
		}
		else {
			//firstAnim = false;
			anim.SetBool ("firstAnim", false);
			anim.SetTrigger("HitDone");
		}
		anim.SetBool ("MeleeAttackInQueue", false);
        //MeleeAttackInQueue = false;
    }


	//Called durch Input
    public void punch()
	{
		if (anim.GetBool("firstAnim")) {
			anim.SetBool ("MeleeAttackInQueue", true);
			//MeleeAttackInQueue = true;
		}
		else {
			//firstAnim = true;
			anim.SetBool ("firstAnim", true);
			anim.SetTrigger("MeleeAttack");
            animationRunning = true;
            idleStateExecuted = false;
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
                    if(enemyHealth != null)
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
