using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

	Rigidbody2D rigenemy;
	Rigidbody2D rigplayer;

	public Transform visionCheck;
	public float noticeDistance;
	public bool playerVisible;
	public float fovAngle;

	CharacterMovement actions;


	void Awake(){
		rigplayer  = (Rigidbody2D) GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
		rigenemy  = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
        actions = (CharacterMovement)GetComponent(typeof(CharacterMovement));
	}

	void FixedUpdate(){
		Vector2 centerPlayer = new Vector2 (rigplayer.position.x, rigplayer.position.y + 0.5f);
		float currentAngle = -1;

		//No field of View component
		if (fovAngle != -1) {
			Vector2 forward = visionCheck.position;
			Vector2 direction;
			if (actions.facingRight) {
				forward += new Vector2 (1, 0);
				direction = rigplayer.position - (Vector2)visionCheck.position;
			} else {
				forward += new Vector2 (-1, 0);
				direction = (Vector2)visionCheck.position - rigplayer.position;
			}
			currentAngle = Vector2.Angle (forward, direction);
		}
		RaycastHit2D hit = Physics2D.Raycast (visionCheck.position, centerPlayer - (Vector2)visionCheck.position, noticeDistance);
		if (currentAngle <= fovAngle) {
			if(hit.collider != null)
				playerVisible = hit.collider.gameObject == rigplayer.gameObject;
			else
				playerVisible = false;
		}
		else
			playerVisible = false;
	}
}
