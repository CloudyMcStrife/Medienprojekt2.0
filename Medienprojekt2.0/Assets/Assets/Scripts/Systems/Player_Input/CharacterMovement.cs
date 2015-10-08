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
    AttributeComponent attComp;


	public bool facingRight = false;
    public bool startingRight;
    public float[] rangeAttackCooldown = {1.0f, 1.0f};
	public float[] rollCooldown = {2.0f,2.0f};
	public float rollDuration;
	public bool rolling;
    public float speed = 4.0f;
    public bool grounded;

	public float jumpheight;
    public LayerMask groundMask;
	public Transform groundCheck;
	public float groundCheckRadius;
    float scaling;

	MeleeSystem meleeSys;
	
	// Use this for initialization
	void Awake () {
        facingRight = startingRight;
		rigplayer = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		collplayer = (BoxCollider2D)GetComponent (typeof(BoxCollider2D));
		trans = (Transform)GetComponent (typeof(Transform));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
		meleeSys = (MeleeSystem)GetComponent (typeof(MeleeSystem));
        scaling = transform.localScale.x;
        attComp = (AttributeComponent)GetComponent(typeof(AttributeComponent));
	}

	
	// Update is called once per frame

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);
	}
	void Update () 
	{
		//cooldowns
		if (rollCooldown [0] < rollCooldown [1])
			rollCooldown [0] += Time.deltaTime;
		rolling = rollCooldown [0] < rollDuration;
		if (rangeAttackCooldown [0] < rangeAttackCooldown [1])
			rangeAttackCooldown [0] += Time.deltaTime;

		
	}

    public void move(float movePlayerVector)
    {
        float xScale = scaling;
        if (!rolling)
        {
            rigplayer.velocity = new Vector2(movePlayerVector * speed, rigplayer.velocity.y);
            if (movePlayerVector < 0 && facingRight)
            {
                facingRight = false;
                xScale *= startingRight ? -1 : 1;
                trans.localScale = new Vector3(xScale, trans.localScale.y, trans.localScale.z);
            }
            if (movePlayerVector > 0 && !facingRight)
            {
                facingRight = true;
                xScale *= startingRight ? 1 : -1;
                trans.localScale = new Vector3(xScale, trans.localScale.y, trans.localScale.z);
            }
        }
    }

    public void stopMovement()
    {
        if(grounded)
            rigplayer.velocity = new Vector2(0, rigplayer.velocity.y);
    }

    public void jump()
    {
        if (grounded)
        {
            rigplayer.velocity = new Vector2(rigplayer.velocity.x, jumpheight);
            grounded = false;
        }
    }

    public void roll()
    {
        if (grounded)
        {
            if (rollCooldown[0] >= rollCooldown[1])
            {
                if (facingRight)
                    rigplayer.velocity = new Vector2(speed * 2, rigplayer.velocity.y);
                else
                    rigplayer.velocity = new Vector2(speed * -2, rigplayer.velocity.y);
                rollCooldown[0] = 0;
            }
        }
    }
        
        
public void shoot()
    {
        if (rangeAttackCooldown[0] >= rangeAttackCooldown[1])
        {
            GameObject proj = PPS.getProjectile();
            if (proj != null)
            {
                currentProjectile = proj.GetComponent<Projectile>();
                rangeAttackCooldown[0] = 0;
                currentProjectile.shoot(2.0f, facingRight, Projectile.Shooting_Type.NORMAL);
            }
        }
    }
//Waffe wechseln
public void switchWeapon()
    {
        AmmoIconHandler iconHandler = GameObject.Find("HUD").GetComponent<AmmoIconHandler>();
        //currentProjectile.
        
    }
    
}
