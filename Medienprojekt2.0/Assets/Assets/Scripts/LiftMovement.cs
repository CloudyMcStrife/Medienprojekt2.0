using UnityEngine;
using System.Collections;

public class LiftMovement : MonoBehaviour {

	BoxCollider2D playercoll;
	BoxCollider2D liftcoll;
	Rigidbody2D liftrig;
	Rigidbody2D playerig;
	// Use this for initialization
	void Start () {
		playercoll = (BoxCollider2D)GameObject.FindGameObjectWithTag ("Player").GetComponent (typeof(BoxCollider2D));
		playerig = (Rigidbody2D)GameObject.FindGameObjectWithTag ("Player").GetComponent (typeof(Rigidbody2D));
		liftcoll = (BoxCollider2D) this.gameObject.GetComponent (typeof(BoxCollider2D));
	}
	
	// Update is called once per frame
	void Update () {
		if (liftcoll.IsTouching (playercoll)) {
			playerig.AddForce(new Vector2(0.0f, 4.0f), ForceMode2D.Impulse);
		}
	}
}
