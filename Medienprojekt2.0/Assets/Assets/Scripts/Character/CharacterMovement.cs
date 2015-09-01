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
	public float[] roleCooldown = {2.0f,2.0f};

	public float speed = 4.0f;

	public bool grounded;
	public LayerMask groundMask;
	public float jumpheight;
	public Transform groundCheck;
	public float groundCheckRadius;
	
	// Use this for initialization
	void Awake () {
		rigplayer = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		collplayer = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		trans = (Transform)GetComponent (typeof(Transform));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
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
		if(roleCooldown[0] < roleCooldown[1])
			roleCooldown [0] += Time.deltaTime;
		if(rangeAttackCooldown[0] < rangeAttackCooldown[1])
			rangeAttackCooldown [0] += Time.deltaTime;

		//Funktion für gehen
		if (Input.GetKey ("a") || Input.GetKey ("d")) {
			rigplayer.velocity = new Vector2 (movePlayerVector * speed, rigplayer.velocity.y);
			if(movePlayerVector < 0 && facingRight)
			{
				facingRight = false;
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				trans.localScale = scale;
			}
			if(movePlayerVector > 0 && !facingRight)
			{
				facingRight = true;
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				trans.localScale = scale;
			}
		} else {
			if (grounded)
				rigplayer.velocity = new Vector2 (0, rigplayer.velocity.y);
		}
		//Funktion für Springen
		if(Input.GetKey("w"))
		{
			if(grounded){
				rigplayer.velocity = new Vector2(rigplayer.velocity.x, jumpheight);
				grounded = false;
			}
		}
		//Funktion für Rollen
		if (Input.GetKey ("k")) {
			if (grounded) {
				if (roleCooldown [0] > roleCooldown [1]) {
					rigplayer.AddForce(new Vector2(movePlayerVector * speed * 2, 0), ForceMode2D.Force);
					roleCooldown [0] = 0;
				}
			}
		}
		//Funktion für Schießen
		if (Input.GetKeyDown ("s")) {
			if(rangeAttackCooldown[0] >= rangeAttackCooldown[1]){
				GameObject proj = PPS.getProjectile();
				if(proj!=null)
				{
					currentProjectile = proj.GetComponent<Projectile>();
					rangeAttackCooldown[0] = 0;
					currentProjectile.shoot (rigplayer.gameObject,2.0f);
				}
			}
		}

	}

	public bool isFacingRight()
	{
		return facingRight;
	}
}
