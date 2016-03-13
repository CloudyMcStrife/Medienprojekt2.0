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
        //Check ob wir blocken, aber keine Stamina mehr besitzen
        if (attributes.stamina == 0 && blocking)
            unblock();

        //Kontinuierliche Stamina Kosten beim blocken
        if(blocking)
        {
            attributes.reduceStamina(costPerSecond * Time.deltaTime);
        }   
	}

    //Wird aufgerufen sobald die animation weit genug fortgeschritten ist, damit der Block effekt eintritt
    public void setBlockReady()
    {
        blocking = true;
    }

    //Gleicher Effekt nur Rückwärts
	public void setBlockDone()
	{
		blocking = false;
	}

    //Wird aufgerufen wenn angefangen wird zu blocken
    //es werden die Staminakosten zum Beginnen abgezogen, sofern vorhanden und die Animation gestartet
    public void block()
    {
        if (attributes.stamina > blockStartCost)
        {
            animationRunning = true;
            anim.SetBool("Blocking", true);
            attributes.reduceStamina(blockStartCost);
        }
    }

    //Wird aufgerufen, wenn die Block-Taste losgelassen wird
    public void unblock()
    {
        anim.SetBool("Blocking", false);
		animationRunning = false;
    }

    //Reset FUnktion die einmal aufgerufen wird, wenn in den Idle State zurückgekehrt wird, jedoch nicht mehrmals (idleStateExecuted)
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
    //Benötigt zum queuen unserer ComboAttacken, wenn wir einen MeleeAttack gedrückt haben, führen wir  den nächsten Angriff aus
    //ansonsten hören wir auf zu schlagen, und fallen wieder zurück in den Idlestate
    public void setAnimationDone()
    {
		if (anim.GetBool("MeleeAttackInQueue")) {
				anim.SetTrigger("MeleeAttack");
		}
		else {
			anim.SetBool ("firstAnim", false);
			anim.SetTrigger("HitDone");
		}
		anim.SetBool ("MeleeAttackInQueue", false);
        //MeleeAttackInQueue = false;
    }


	//Called durch Input oder KI
    //Wenn der erste Hit durchgeführt wurde, fängt er an hits zu queuen
    //wenn es der erste Hit ist, beginnt er die Animationen
    public void punch()
	{
		if (anim.GetBool("firstAnim")) {
			anim.SetBool ("MeleeAttackInQueue", true);
		}
		else {
			anim.SetBool ("firstAnim", true);
			anim.SetTrigger("MeleeAttack");
            animationRunning = true;
            idleStateExecuted = false;
		}
	}

    //Funktion die zum Schaden auswirken aufgerufen wird
    //wird durch den Keyframe der Animation aufgerufen, an dem Zeitpunkt, an dem der Schaden entstehen soll...
	public void applyMeleeDamage()
	{
		Vector2 direction;
		
		if (movement.facingRight)
			direction = new Vector2 (1, 0);
		else
			direction = new Vector2 (-1, 0);


        //Raycast von unserem Melee Waffenpunkt aus, in Blickrichtung in Länge der Schlagrichtung
        RaycastHit2D[] hits = Physics2D.RaycastAll(meleePoint.position, direction, hitrange);
        //Debug.DrawRay(meleePoint.position, direction);
        //Debug.Log(hits.Length);
        foreach (RaycastHit2D c in hits)
            {
                GameObject hittedObject = c.collider.gameObject;
                Debug.Log(hittedObject.name);
                if (hittedObject.layer == opponentLayer)
                {
                    HealthSystem hittedHealth = (HealthSystem)hittedObject.GetComponent(typeof(HealthSystem));
                    if (hittedHealth != null)
                        hittedHealth.lowerHealth(attributes.getDamage());
                }
            }
        }
}
