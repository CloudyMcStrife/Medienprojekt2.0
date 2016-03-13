using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

	Rigidbody2D rigenemy;
	Rigidbody2D rigplayer;


    //Punkt der Augen
	public Transform visionCheck;
	public float noticeDistance;
	public bool playerVisible;

    //Winkel in dem sich das Ziel maximal befinden darf, um erkannt zu werden.
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
            //Position des Spielers
            Vector2 playerPos = rigplayer.position;

            //Position unserer  Augen
            Vector2 visionPos = visionCheck.position;


            Vector2 viewVector = new Vector2(0, 0);

            //Verbindungsvektor zwischen Augen und Ziel/Spieler
            Vector2 difference = playerPos - visionPos;

            //Blickrichtung entweder x=1 oder -1
            if (actions.facingRight)
                viewVector += new Vector2(1, 0);
            else
                viewVector += new Vector2(-1, 0);

            //Winkel zwischen Blickrichtung und Verbindungsvektor
            float currentAngle = Vector2.Angle(viewVector, difference);
            RaycastHit2D hit = Physics2D.Raycast(visionCheck.position,viewVector, noticeDistance);

            //Überprüfen ob der Winkel maximal dem angegebenen Field of View Winkel ist
            if (currentAngle <= fovAngle)
            {
                //Raycast der von Augen zum Ziel geschossen wird, wenn das erste getroffene Objekt der Spieler ist, ist die sicht nicht blockiert
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
