using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

	Rigidbody2D rigenemy;
	Rigidbody2D rigplayer;

	public Transform visionCheck;
	public float noticeDistance;
	public bool playerVisible;

	EnemyMovement movement;


	void Awake(){
		rigplayer  = (Rigidbody2D) GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
		rigenemy  = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		movement = (EnemyMovement)GetComponent (typeof(EnemyMovement));
	}

	void FixedUpdate(){
		Vector3 forward = movement.isFacingRight() ? new Vector3 (1, 0, 0) : new Vector3 (-1, 0, 0);
		RaycastHit2D hit = Physics2D.Raycast(visionCheck.position,forward,noticeDistance);
		playerVisible = false;
		if (hit.collider != null) {
			playerVisible = hit.collider.gameObject == rigplayer.gameObject;
		}
	}

	public bool isPlayerVisible(){
		return playerVisible;
	}
}
