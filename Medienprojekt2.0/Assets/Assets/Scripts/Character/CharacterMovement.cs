using UnityEngine;
using System.Collections;

//Beinhaltet alle Aktionen die der Spieler ausführen kann, dieses Script ist auch nur teil vom Spieler.

public class CharacterMovement : MonoBehaviour {

	Rigidbody2D rigplayer;
	BoxCollider2D collplayer;
	Transform trans;
	GameObject weapon;
	Rigidbody2D rigweapon;
	ShootBehaviour shootbe;
	ProjectilePoolingSystem PPS;

	public float[] rangeAttackCooldown = {1.0f, 1.0f};
	public float[] roleCooldown = {2.0f,2.0f};
	public bool jumping;
	public float speed = 4.0f;
	public bool isFacingRight = false;
	
	// Use this for initialization
	void Awake () {
		rigplayer = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		collplayer = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		trans = (Transform)GetComponent (typeof(Transform));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
	}

	
	// Update is called once per frame
	void Update () 
	{
		float movePlayerVector = Input.GetAxis ("Horizontal");

		Ray2D rayDown = new Ray2D (this.transform.position, new Vector2 (0, -1));
		RaycastHit2D a = Physics2D.Raycast (this.transform.position, new Vector2 (0, -1));

		if (a.collider != null) {
			// Calculate the distance from the surface and the "error" relative
			// to the floating height.
			float distance = Mathf.Abs(a.point.y - transform.position.y);
			var heightError = 3 - distance;
			
			// The force is proportional to the height error, but we remove a part of it
			// according to the object's speed.
			var force = 2 * heightError - rigplayer.velocity.y * 1;
			
			// Apply the force to the rigidbody.
			rigplayer.AddForce(Vector3.up * force);
		}

		//cooldowns
		roleCooldown [0] += Time.deltaTime;
		rangeAttackCooldown [0] += Time.deltaTime;

		//Funktion für gehen
		if (Input.GetKey ("a") || Input.GetKey ("d")) {
			rigplayer.velocity = new Vector2 (movePlayerVector * speed, rigplayer.velocity.y);
			if(movePlayerVector < 0 && isFacingRight)
			{
				isFacingRight = false;
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				trans.localScale = scale;
			}
			if(movePlayerVector > 0 && !isFacingRight)
			{
				isFacingRight = true;
				Vector3 scale = transform.localScale;
				scale.x *= -1;
				trans.localScale = scale;
			}
		} else {
			if (!jumping)
				rigplayer.velocity = new Vector2 (0, rigplayer.velocity.y);
		}
		//Funktion für Springen
		if(Input.GetKey("w"))
		{
			if(!jumping){
				rigplayer.velocity = new Vector2(movePlayerVector * speed, 6);
				jumping = true;
			}
		}
		//Funktion für Rollen
		if (Input.GetKey ("k")) {
			if (!jumping) {
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
				foreach(Component compo in proj.GetComponents<Component>()){
					Debug.Log (compo.GetType());
				}
				shootbe = proj.GetComponent<ShootBehaviour>();
				rangeAttackCooldown[0] = 0;
				Debug.Log(shootbe);      
				shootbe.shoot (rigplayer.gameObject);
			}
		}

	}

	public bool getIsFacingRight()
	{
		return isFacingRight;
	}
}
