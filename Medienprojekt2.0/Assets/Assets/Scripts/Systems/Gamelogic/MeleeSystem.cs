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


    public Transform meleePoint;
    public float hitrange;
    int opponentLayer;
    float hitTime;
    bool idleStateExecuted;

	// Use this for initialization
	void Awake () {
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
		movement = (CharacterMovement)GetComponent (typeof(CharacterMovement));
        rigidBody = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        anim = (Animator)GetComponent(typeof(Animator));
        idleStateExecuted = false;

        int enemyLayer = LayerMask.NameToLayer("Enemies");
        int playerLayer = LayerMask.NameToLayer("Player");

        //wenn wir spieler sind, sind gegner gegner, ansonsten is der spieler der gegner
        if (this.gameObject.layer == enemyLayer)
            opponentLayer = playerLayer;
        else
            opponentLayer = enemyLayer;
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


	//Called durch Input oder KI
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


    //used 
    public void onePunch()
    {
            anim.SetBool("firstAnim", true);
            anim.SetTrigger("MeleeAttack");
            animationRunning = true;
            idleStateExecuted = false;
    }

    public void enemyHit(GameObject objectHit)
    {
        HealthSystem enemyHealth = (HealthSystem)objectHit.GetComponent(typeof(HealthSystem));
        if (enemyHealth != null)
            enemyHealth.lowerHealth(attributes.getDamage());
        Debug.Log("Meleehit");
    }


	public void applyMeleeDamage()
	{
		Vector2 direction;
		
		if (movement.facingRight)
			direction = new Vector2 (1, 0);
		else
			direction = new Vector2 (-1, 0);

        RaycastHit2D[] hits = Physics2D.RaycastAll(meleePoint.position, direction, hitrange);
        Debug.DrawRay(meleePoint.position, direction);
        Debug.Log(hits.Length);
        foreach (RaycastHit2D c in hits)
            {
            GameObject hittedObject = c.collider.gameObject;
            Debug.Log(hittedObject.name);
                if (hittedObject.layer == opponentLayer)
                {
                    HealthSystem hittedHealth = (HealthSystem)hittedObject.GetComponent(typeof(HealthSystem));
                    if (hittedHealth != null)
                        hittedHealth.lowerHealth(attributes.getDamage());
                    Debug.Log("Meleehit");
                }
            }
        }
}
