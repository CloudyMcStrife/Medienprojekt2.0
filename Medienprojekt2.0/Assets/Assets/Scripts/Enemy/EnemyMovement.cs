using UnityEngine;
using System.Collections;

//Beschreibt alle Aktionen die Gegner ausführen können

public class EnemyMovement : MonoBehaviour {
	
	Rigidbody2D rigplayer;
	Rigidbody2D rigenemy;
	float speed = 1.0f;


	//AI Settings
	public float noticeDistance;
	//Attack Range
	public float minimumDistancex;
	public float minimumDistancey;
	public bool inAttackRangex;
	public bool inAttackRangey;
	public bool inNoticeRadius;
	float distancex;
	float distancey;


	public LayerMask wallMask;
	public Transform wallCheck;
	public float wallCheckRadius;

	public LayerMask groundMask;
	public Transform groundCheck;
	public float groundCheckRadius;


	bool walkingRight;
	bool hittingWall;
	bool onAnEdge;

	//Shooting
	public float[] attackCooldown = {1.0f,1.0f};
	ProjectilePoolingSystem PPS;
	Projectile currentProjectile;




	// Use this for initialization
	void Awake () {
		walkingRight = false;
		rigplayer  = (Rigidbody2D) GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
		rigenemy  = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
	}
	
	// Update is called once per frame
	void Update () {

		if (attackCooldown [0] < attackCooldown [1]) {
			attackCooldown [0] += Time.deltaTime;
		}


		distancex = rigplayer.position.x - rigenemy.position.x;
		distancey = rigplayer.position.y - rigenemy.position.y;
		//prüft ob EnemyEntity links oder rechts in minimumDistance (=Angriffsreichweite) ist;
		inAttackRangex = (distancex <= minimumDistancex && distancex > 0) || (distancex >= -minimumDistancex && distancex < 0);
		inAttackRangey = (distancey <= minimumDistancey && distancey > 0) || (distancey >= -minimumDistancey && distancey < 0);
		inNoticeRadius = (distancex <= noticeDistance && distancex > 0) || (distancex >= -noticeDistance && distancex < 0);


		//Begin Chasing
		if (inNoticeRadius && !inAttackRangex) {
			if (distancex > 0)
				rigenemy.velocity = new Vector2 (speed, 0);
			if (distancex < 0)
				rigenemy.velocity = new Vector2 (-speed, 0);
		}


		//Should be able to attack now, if cooldowns are up
		if (inAttackRangex && inAttackRangey) {
			rigenemy.velocity = new Vector2 (0, rigenemy.velocity.y);
			if (attackCooldown [0] >= attackCooldown [1]) {
				//Setzt das Script auf das Projektil das als nächstes Geschossen werden soll(Oberstes des PPS)
				GameObject projectile = PPS.getProjectile ();
				if (projectile != null) {
					currentProjectile = (Projectile)projectile.GetComponent (typeof(Projectile));
					attackCooldown [0] = 0;
					currentProjectile.shoot (rigenemy.gameObject, minimumDistancex);
				}
			}
		}


		//Patrolling behaviour if not attacking or chasing
		if(!(inAttackRangex && inAttackRangey) && !inNoticeRadius) {
			hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, wallMask);
			onAnEdge = !Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);
			if(hittingWall || onAnEdge)
				walkingRight = !walkingRight;
			if(walkingRight)
			{
				transform.localScale = new Vector3(-1f,1f,1f);
				rigenemy.velocity = new Vector2(speed,rigenemy.velocity.y);
			}
			else
			{
				transform.localScale = new Vector3(1f,1f,1f);
				rigenemy.velocity = new Vector2(-speed,rigenemy.velocity.y);
			}
		}
	}

	public bool isFacingRight()
	{
		return distancex > 0;
	}
}
