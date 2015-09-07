using UnityEngine;
using System.Collections;

//Beinhaltet das Verhalten eines Projektils, wird auch nur an Projektile angehangen. Dient zb. zu Trefferabfrage bei schüssen.

public class Projectile : MonoBehaviour {
	BoxCollider2D collid;
	SpriteRenderer sprite;
	Rigidbody2D rigid;
	EnemyMovement enemyMove;
	CharacterMovement charMove;
	GameObject prObject; //GameObject des Projektils
	GameObject owner;
	float projectileSpeed = 10.0f;
	float startX;
	float damage;
	float range;
	public bool inAir =false;

	// Use this for initialization
	void Start () {
		collid = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		sprite = (SpriteRenderer)GetComponent (typeof(SpriteRenderer));
		rigid = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));
		prObject = this.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Verringert HP und gibt das Projektil zurück in den Pool wenn der Spieler getroffen wurde
		if (collid.IsTouching ((BoxCollider2D)GameObject.FindWithTag ("Player").GetComponent (typeof(BoxCollider2D)))) {
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			HealthSystem HS = (HealthSystem)GameObject.FindWithTag ("Player").GetComponent (typeof(HealthSystem));
			HS.lowerHealth(this.getDamage());
			PPS.storeProjectile(prObject);
			inAir = false;
		}else
			//Gibt das Projektil zurück in den Pool wenn eine Wand getroffen wurde
		if (collid.IsTouchingLayers(LayerMask.NameToLayer("Walls"))){
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			Debug.Log ("WALL OR GROUND");
			PPS.storeProjectile(prObject);
			inAir = false;
			
		}else

		if(collid.IsTouching ((BoxCollider2D)GameObject.FindWithTag ("Enemy").GetComponent (typeof(BoxCollider2D)))){
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			PPS.storeProjectile(prObject);
			Debug.Log ("hitEnemy");
			inAir = false;
		}else

		if (Mathf.Abs(startX - collid.transform.position.x) >= range && inAir) {
			ProjectilePoolingSystem PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
			PPS.storeProjectile(prObject);
			inAir = false;
		}
	}

	public void shoot(GameObject owner,float range)
	{
		this.owner = owner;
		this.range = range;
		Physics2D.IgnoreCollision ((Collider2D)owner.GetComponent(typeof(Collider2D)), collid);
		AttributeComponent ac = (AttributeComponent)owner.GetComponent (typeof(AttributeComponent));
		setDamage (ac.getDamage ());

		inAir = true;
		rigid.transform.position = new Vector2(owner.transform.position.x,owner.transform.position.y +.5f);
		startX = owner.transform.position.x;


		if (owner == GameObject.FindWithTag ("Enemy")) {
			enemyMove = (EnemyMovement)owner.GetComponent (typeof(EnemyMovement));
			if (enemyMove.isFacingRight()) {
				rigid.velocity = new Vector2 (projectileSpeed, 0);
			} else
				rigid.velocity = new Vector2 (-projectileSpeed, 0);
		}
		if (owner == GameObject.FindWithTag ("Player")) {
			charMove = (CharacterMovement)owner.GetComponent (typeof(CharacterMovement));
			if (charMove.isFacingRight()) {
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
