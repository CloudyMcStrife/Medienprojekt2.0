using UnityEngine;
using System.Collections;

public class InputSystem : MonoBehaviour {

    CharacterMovement movement;
    AttributeComponent attComp;
    RangedSystem rangedSys;
    MeleeSystem meleeSys;
    Collider2D playerColl;
    AbilitySystem abilitySys;
    bool primaryShot = true;
    

	// Use this for initialization
	void Start () {
        attComp = (AttributeComponent)gameObject.GetComponent(typeof(AttributeComponent));
        movement = (CharacterMovement)gameObject.GetComponent(typeof(CharacterMovement));
        meleeSys = (MeleeSystem)gameObject.GetComponent(typeof(MeleeSystem));
        rangedSys = (RangedSystem)GetComponent(typeof(RangedSystem));
        playerColl = (Collider2D)GetComponent(typeof(Collider2D));
        abilitySys = (AbilitySystem)GetComponent(typeof(AbilitySystem));
	}
	
	// Update is called once per frame
    void Update()
    {
        float movePlayerVector = Input.GetAxis("Horizontal");
		bool isFacingRight;
		if (movePlayerVector >= 0)
			isFacingRight = true;
		else
			isFacingRight = false;
        

        //Funktion für Springen
        if (Input.GetKey("w"))
        {
			if (!meleeSys.blocking)
            	StartCoroutine(movement.jump());
        }
        //Funktion für Rollen
        if (Input.GetKey("k"))
        {
            movement.roll();
        }
        //Funktion für Schießen
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(rangedSys.shoot(primaryShot));
        }

        if (Input.GetKeyDown("b"))
        {
            meleeSys.block();
        }

        if (Input.GetKeyUp("b"))
        {
            meleeSys.unblock();
        }

        if (Input.GetKeyDown("c"))
		{
			rangedSys.switchWeapon();
            primaryShot = !primaryShot;
		}

        if(Input.GetKeyDown("k"))
        {
            if (!attComp.getCooldown2Active())
            {
                abilitySys.clone();
            }
            
            
        }

		if (Input.GetKeyDown ("j")) 
		{
            if(movement.grounded && meleeSys.anim != null && !meleeSys.anim.GetBool("MeleeAttackInQueue"))
            {
                    movePlayerVector = 0.0f;
                    meleeSys.punch();
            }
		}


		if (meleeSys.animationRunning || meleeSys.blocking)
			movePlayerVector = 0.0f;
        movement.move(movePlayerVector);
    }
}
