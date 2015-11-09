using UnityEngine;
using System.Collections;

//Beinhaltet das Verhalten eines Projektils, wird auch nur an Projektile angehangen. Dient zb. zu Trefferabfrage bei schüssen.

public class Projectile : MonoBehaviour {

    public enum Shooting_Type
    {
        NORMAL,
        SPECIAL
    }

    //Allgemeine Kugel Eigenschaften
    GameObject prObject; //GameObject des Projektils
	Rigidbody2D rigid;
	BoxCollider2D collid;
	ProjectilePoolingSystem PPS;

    Shooting_Type s_type;

    Sprite normal_shot;
    Sprite special_shot;

	//Schussspezifische Eigenschaften
	GameObject owner;
	float projectileSpeed = 10.0f;
	float damage;
	float range;
	float timeLeft;
	bool inAir = false;


	// Use this for initialization
	void Start () {
		collid = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		rigid = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));

		prObject = this.gameObject;

        s_type = Shooting_Type.NORMAL;

        normal_shot = Resources.Load<Sprite>("Sprites/Projectile/Normal_Shot") as Sprite;
        special_shot = Resources.Load<Sprite>("Sprites/Projectile/Special_Shot") as Sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (inAir) {
			if(timeLeft <= 0)
			{
				inAir = false;
				PPS.storeProjectile(prObject);
			}
			timeLeft-=Time.deltaTime;
		}
	}

	public void shoot(float range,bool facingRight)
	{
		this.range = range;
		timeLeft = range / projectileSpeed;
		Physics2D.IgnoreCollision ((Collider2D)owner.GetComponent(typeof(Collider2D)), collid);
		AttributeComponent ac = (AttributeComponent)owner.GetComponent (typeof(AttributeComponent));
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sortingOrder = 2;
        
        if(s_type == Shooting_Type.NORMAL)
        {
            projectileSpeed = 10.0f;
            damage = ac.getDamage();
            sr.sprite = normal_shot;
        }
        else
        {
            ac.setCooldown1Active(true);
            damage = ac.getDamage() * 2;
            projectileSpeed = 6.0f;
            sr.sprite = special_shot;
        }
		setDamage (damage);

		inAir = true;
        rigid.transform.position = PPS.WeaponPoint.transform.position;


		if (facingRight) 
			rigid.velocity = new Vector2 (projectileSpeed, 0);
		else
			rigid.velocity = new Vector2 (-projectileSpeed, 0);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		PPS.storeProjectile (prObject);
		HealthSystem HS = (HealthSystem)coll.gameObject.GetComponent (typeof(HealthSystem));
		if (HS != null)
			HS.lowerHealth (getDamage());
		GameObject hiteffect = Instantiate (GameObject.Find ("HitEffect01"));
		Transform TS = (Transform) coll.gameObject.GetComponent (typeof(Transform));
		hiteffect.transform.position = new Vector3 (TS.position.x, TS.position.y + 0.5f, TS.position.z);
		Destroy (hiteffect, 2);
		inAir = false;
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

	public void setOwner(GameObject owner)
	{
		this.owner = owner;
		PPS = (ProjectilePoolingSystem) owner.GetComponent(typeof(ProjectilePoolingSystem));
	}

    public Shooting_Type get_shooting_type()
    {
        return s_type;
    }

    public void set_shooting_type(Shooting_Type type)
    {
        s_type = type;
    }
}
