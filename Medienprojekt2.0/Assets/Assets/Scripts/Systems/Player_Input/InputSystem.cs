using UnityEngine;
using System.Collections;

public class InputSystem : MonoBehaviour {

    CharacterMovement actions;
    MeleeSystem meleesys;
	// Use this for initialization
	void Start () {
        actions = (CharacterMovement)gameObject.GetComponent(typeof(CharacterMovement));
        meleesys = (MeleeSystem)gameObject.GetComponent(typeof(MeleeSystem));
	}
	
	// Update is called once per frame
    void Update()
    {
        float movePlayerVector = Input.GetAxis("Horizontal");
        

        //Funktion für Springen
        if (Input.GetKey("w"))
        {
            actions.jump();
        }
        //Funktion für Rollen
        if (Input.GetKey("k"))
        {
            actions.roll();
        }
        //Funktion für Schießen
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(actions.shoot(true));
        }

        if (Input.GetKeyDown("p"))
        {
            StartCoroutine(actions.shoot(true));
        }

        if (Input.GetKeyDown("b"))
        {
            movePlayerVector = 0.0f;
            meleesys.block();
        }

        if (Input.GetKeyUp("b"))
        {
            meleesys.unblock();
        }

        if (Input.GetKeyDown("c"))
		{
			actions.switchWeapon();
		}



        actions.move(movePlayerVector);
    }
}
