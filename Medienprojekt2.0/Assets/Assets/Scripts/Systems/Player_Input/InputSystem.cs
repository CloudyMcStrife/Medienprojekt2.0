using UnityEngine;
using System.Collections;

public class InputSystem : MonoBehaviour {

    CharacterMovement movement;
    RangedSystem rangedSys;
    MeleeSystem meleeSys;
    bool primaryShot = true;

	// Use this for initialization
	void Start () {
        movement = (CharacterMovement)gameObject.GetComponent(typeof(CharacterMovement));
        meleeSys = (MeleeSystem)gameObject.GetComponent(typeof(MeleeSystem));
        rangedSys = (RangedSystem)GetComponent(typeof(RangedSystem));
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

		if (Input.GetKeyDown ("j")) 
		{
			if(!meleeSys.MeleeAttackInQueue)
				meleeSys.punch();
		}

		if (meleeSys.blockAction || meleeSys.blocking)
			movePlayerVector = 0.0f;
        movement.move(movePlayerVector);
    }
}
