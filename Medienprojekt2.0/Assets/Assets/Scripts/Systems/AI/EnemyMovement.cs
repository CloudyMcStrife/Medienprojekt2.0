using UnityEngine;
using System.Collections;

//Beschreibt alle Aktionen die Gegner ausführen können

public class EnemyMovement : MonoBehaviour {
	
	Rigidbody2D rigplayer;
	Rigidbody2D rigenemy;
	EnemyVision vision;

	//Movement
	bool walkingRight;
	float speed = 1.0f;


	//AI Settings
	//Attack Range
	public float minimumDistancex;
	public float minimumDistancey;
	bool inAttackRangex;
	bool inAttackRangey;
	float distancex;
	float distancey;


	public LayerMask wallMask;
	public Transform wallCheck;
	public float wallCheckRadius;
	bool hittingWall;

	public LayerMask groundMask;
	public Transform groundCheck;
	public float groundCheckRadius;
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
		vision = (EnemyVision)GetComponent (typeof(EnemyVision));
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		distancex = rigplayer.position.x - rigenemy.position.x;
		distancey = rigplayer.position.y - rigenemy.position.y;
		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, wallMask);
		onAnEdge = !Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);
	}


	void Update () {
		if (attackCooldown [0] < attackCooldown [1]) {
			attackCooldown [0] += Time.deltaTime;
		}


		
		//prüft ob EnemyEntity links oder rechts in minimumDistance (=Angriffsreichweite) ist;
		inAttackRangex = (distancex <= minimumDistancex && distancex > 0) || (distancex >= -minimumDistancex && distancex < 0);
		inAttackRangey = (distancey <= minimumDistancey && distancey > 0) || (distancey >= -minimumDistancey && distancey < 0);


		//Begin Chasing
		if (!inAttackRangex && vision.isPlayerVisible()) {
			if (walkingRight)
				rigenemy.velocity = new Vector2 (speed, 0);
			else
				rigenemy.velocity = new Vector2 (-speed, 0);
		}


		//Should be able to attack now, if cooldowns are up
		else if ( (inAttackRangex) && vision.isPlayerVisible()) {
			rigenemy.velocity = new Vector2 (0, rigenemy.velocity.y);
			if (attackCooldown [0] >= attackCooldown [1]) {
				GameObject projectile = PPS.getProjectile ();
				if (projectile != null) {
					currentProjectile = (Projectile)projectile.GetComponent (typeof(Projectile));
					attackCooldown [0] = 0;
					currentProjectile.shoot(minimumDistancex,walkingRight, Projectile.Shooting_Type.NORMAL);
				}
			}
		}


		//Patrolling behaviour if not attacking or chasing
		if( !vision.isPlayerVisible() ) {
            if (hittingWall || onAnEdge)
            {
                walkingRight = !walkingRight;
            }
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
		return walkingRight;
	}
}
