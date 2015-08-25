using UnityEngine;
using System.Collections;

//Beinhaltet das Verhalten eines Projektils, wird auch nur an Projektile angehangen. Dient zb. zu Trefferabfrage bei schüssen.

public class ShootBehaviour : MonoBehaviour {
	BoxCollider2D collid;
	SpriteRenderer sprite;
	Rigidbody2D rigid;
	EnemyMovement movement;
	CharacterMovement move;
	GameObject owner;
	float projectileSpeed = 10.0f;
	float startX;
	float startY;
	float damage;
	float range = 7;
	public bool inAir =false;
	public float cooldownTimer = 0.0f;
	const float cooldown = 3.0f;

	// Use this for initialization
	void Start () {
		collid = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		sprite = (SpriteRenderer)GetComponent (typeof(SpriteRenderer));
		rigid = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));
	}
	
	// Update is called once per frame
	void Update () {
		//Verringert HP und gibt das Projektil zurück in den Pool wenn der Spieler getroffen wurde
		if (collid.IsTouching ((BoxCollider2D)GameObject.FindWithTag ("Player").GetComponent (typeof(BoxCollider2D))) && owner != GameObject.FindWithTag ("Player")) {
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			HealthSystem HS = (HealthSystem)GameObject.FindWithTag ("Player").GetComponent (typeof(HealthSystem));
			HS.lowerHealth(this.getDamage());
			PPS.storeProjectile(sprite.gameObject);
			Debug.Log ("hitPlayer");
			inAir = false;
		}else
			//Gibt das Projektil zurück in den Pool wenn eine Wand getroffen wurde
		if (collid.IsTouching((BoxCollider2D)GameObject.FindWithTag("Wall").GetComponent(typeof(BoxCollider2D)))){
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			PPS.storeProjectile(sprite.gameObject);
			Debug.Log ("hitWall");
			inAir = false;
			
		}else

		if(collid.IsTouching ((BoxCollider2D)GameObject.FindWithTag ("Enemy").GetComponent (typeof(BoxCollider2D))) && owner != GameObject.FindWithTag ("Enemy")){
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			PPS.storeProjectile(sprite.gameObject);
			Debug.Log ("hitEnemy");
			inAir = false;
		}else

		if (cooldownTimer >= cooldown && owner != null && inAir) {
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			PPS.storeProjectile(sprite.gameObject);
			inAir = false;
			cooldownTimer = 0.0f;
		}
		if(inAir)
			cooldownTimer += Time.deltaTime;
	}

	public void shoot(GameObject shooter)
	{
		AttributeComponent ac = (AttributeComponent)shooter.GetComponent (typeof(AttributeComponent));
		setDamage (ac.getDamage ());

		owner = shooter;
		if (owner == GameObject.FindWithTag ("Enemy")) {
			movement = (EnemyMovement)shooter.GetComponent (typeof(EnemyMovement));
			inAir = true;
			rigid.transform.position = new Vector2(shooter.transform.position.x,shooter.transform.position.y +.5f);
			startX = shooter.transform.position.x;
			if (movement.isFacingRight()) {
				rigid.velocity = new Vector2 (projectileSpeed, 0);
			} else
				rigid.velocity = new Vector2 (-projectileSpeed, 0);
		}
		if (owner == GameObject.FindWithTag ("Player")) {
			move = (CharacterMovement)shooter.GetComponent (typeof(CharacterMovement));
			inAir = true;
			rigid.transform.position = new Vector2(shooter.transform.position.x,shooter.transform.position.y +.5f);
			if (move.getIsFacingRight()) {
				rigid.velocity = new Vector2 (projectileSpeed, 0);
			} else
				rigid.velocity = new Vector2 (-projectileSpeed, 0);

		}
	}

	//Übergibt Schaden des Projektils(Genutzt in HealthSystem)
	public float getDamage()
	{
		return damage;
	}

	//setzt Schaden des Projektils
	public void setDamage(float newDamage)
	{
		damage = newDamage;
	}
}
