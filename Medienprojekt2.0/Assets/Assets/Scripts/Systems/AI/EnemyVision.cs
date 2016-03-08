using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

	Rigidbody2D rigenemy;
	Rigidbody2D rigplayer;

	public Transform visionCheck;
	public float noticeDistance;
	public bool playerVisible;
	public float fovAngle = 180;

	CharacterMovement actions;


	void Awake(){
		rigplayer  = (Rigidbody2D) GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
		rigenemy  = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
        actions = (CharacterMovement)GetComponent(typeof(CharacterMovement));
	}

	void FixedUpdate(){
        if (rigplayer != null)
        {
            Vector2 playerPos = rigplayer.position;
            Vector2 visionPos = visionCheck.position;
            Vector2 viewVector = new Vector2(0, 0);
            Vector2 difference = playerPos - visionPos;
            if (actions.facingRight)
                viewVector += new Vector2(1, 0);
            else
                viewVector += new Vector2(-1, 0);

            float currentAngle = Vector2.Angle(viewVector, difference);
            RaycastHit2D hit = Physics2D.Raycast(visionCheck.position,viewVector, noticeDistance);
            if (currentAngle <= fovAngle)
            {
                if (hit.collider != null)
                    playerVisible = hit.collider.gameObject == rigplayer.gameObject;
                else
                    playerVisible = false;
            }
            else
                playerVisible = false;
        }
	}

    void Update()
    {
        if(GameObject.Find("Klon"))
        {
            rigplayer = (Rigidbody2D)GameObject.Find("Klon").GetComponent(typeof(Rigidbody2D));
        }
        else
        {
            rigplayer = (Rigidbody2D)GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
        }
    }
}
