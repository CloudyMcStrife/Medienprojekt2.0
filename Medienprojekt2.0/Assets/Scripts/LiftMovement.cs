using UnityEngine;
using System.Collections;

public class LiftMovement : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    //Fügt dem Spieler auf der y-Achse geschwindigkeit hinzu
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player")
			other.attachedRigidbody.velocity = new Vector2 (other.attachedRigidbody.velocity.x, other.attachedRigidbody.velocity.y+0.3f);
	}
}