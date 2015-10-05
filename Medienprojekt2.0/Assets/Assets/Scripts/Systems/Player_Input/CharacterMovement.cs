using UnityEngine;
using System.Collections;

//Beinhaltet alle Aktionen die der Spieler ausführen kann, dieses Script ist auch nur teil vom Spieler.

public class CharacterMovement : MonoBehaviour {

	Rigidbody2D rigplayer;
	BoxCollider2D collplayer;
	Transform trans;
	GameObject weapon;
	Rigidbody2D rigweapon;
	Projectile currentProjectile;
	ProjectilePoolingSystem PPS;
	bool facingRight = false;


	public float[] rangeAttackCooldown = {1.0f, 1.0f};
	public float[] rollCooldown = {2.0f,2.0f};
	public float rollDuration;
	bool rolling;

	public float speed = 4.0f;

	public bool grounded;
	public LayerMask groundMask;
	public float jumpheight;
	public Transform groundCheck;
	public float groundCheckRadius;

	MeleeSystem meleeSys;
	
	// Use this for initialization
	void Awake () {
		rigplayer = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		collplayer = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		trans = (Transform)GetComponent (typeof(Transform));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
		meleeSys = (MeleeSystem)GetComponent (typeof(MeleeSystem));
	}

	
	// Update is called once per frame

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);
	}
	void Update () 
	{
		float movePlayerVector = Input.GetAxis ("Horizontal");
		//cooldowns
		if (rollCooldown [0] < rollCooldown [1])
			rollCooldown [0] += Time.deltaTime;
		rolling = rollCooldown [0] < rollDuration;
		if (rangeAttackCooldown [0] < rangeAttackCooldown [1])
			rangeAttackCooldown [0] += Time.deltaTime;

		//Funktion für gehen
		if (!rolling) {
			if (Input.GetKey ("a") || Input.GetKey ("d")) {
				rigplayer.velocity = new Vector2 (movePlayerVector * speed, rigplayer.velocity.y);
				if (movePlayerVector < 0 && facingRight) {
					facingRight = false;
					trans.localScale = new Vector3 (-Mathf.Abs(trans.localScale.x), trans.localScale.y, trans.localScale.z);
				}
				if (movePlayerVector > 0 && !facingRight) {
					facingRight = true;
					trans.localScale = new Vector3 (Mathf.Abs(trans.localScale.x), trans.localScale.y, trans.localScale.z);
				}
			} else {
				if (grounded)
					rigplayer.velocity = new Vector2 (0, rigplayer.velocity.y);
			}
		}

		//Funktion für Springen
		if (Input.GetKey ("w")) {
			if (grounded) {
				rigplayer.velocity = new Vector2 (rigplayer.velocity.x, jumpheight);
				grounded = false;
			}
		}
		//Funktion für Rollen
		if (Input.GetKey ("k")) {
			if (grounded) {
				if (rollCooldown [0] >= rollCooldown [1]) {
					if (facingRight)
						rigplayer.velocity = new Vector2 (speed * 2, rigplayer.velocity.y);
					else
						rigplayer.velocity = new Vector2 (speed * -2, rigplayer.velocity.y);
					rollCooldown [0] = 0;
				}
			}
		}
		//Funktion für Schießen
		if (Input.GetKeyDown ("s")) {
			if (rangeAttackCooldown [0] >= rangeAttackCooldown [1]) {
				GameObject proj = PPS.getProjectile ();
				if (proj != null) {
					currentProjectile = proj.GetComponent<Projectile> ();
					rangeAttackCooldown [0] = 0;
					currentProjectile.shoot (2.0f,facingRight,Projectile.Shooting_Type.NORMAL);
				}
			}
		}

		//Blocken
		if (Input.GetKeyDown ("f")) {
			meleeSys.block (true);
		}
		if (Input.GetKeyUp ("f")) {
			meleeSys.block (false);
		}
	}

	public bool isFacingRight()
	{
		return facingRight;
	}
}
