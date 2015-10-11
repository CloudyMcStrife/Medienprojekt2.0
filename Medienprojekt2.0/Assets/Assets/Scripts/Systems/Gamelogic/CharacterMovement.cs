using UnityEngine;
using System.Collections;

//Beinhaltet alle Aktionen die der Spieler ausführen kann, dieses Script ist auch nur teil vom Spieler.

public class CharacterMovement : MonoBehaviour {

	Rigidbody2D rigplayer;
	Transform trans;
	GameObject weapon;
	Rigidbody2D rigweapon;
	Projectile currentProjectile;
	ProjectilePoolingSystem PPS;
    Animator anim;
    AttributeComponent attributes;


	public bool facingRight = true;
    public bool startingRight;
    public float[] rangeAttackCooldown = {1.0f, 1.0f};
    bool shotAnimationReady;
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
	
	// Use this for initialization
	void Awake () {
        facingRight = startingRight;
		rigplayer = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		trans = (Transform)GetComponent (typeof(Transform));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
        anim = (Animator)GetComponent(typeof(Animator));
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
        scaling = transform.localScale.x;
        shotAnimationReady = false;
	}

	
	// Update is called once per frame

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);
        if (anim != null)
            anim.SetBool("Grounded", grounded);
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
        float newSpeed = movePlayerVector * speed;
        if (!rolling)
        {
            rigplayer.velocity = new Vector2(newSpeed, rigplayer.velocity.y);
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
        if (anim != null)
            anim.SetFloat("Speedx",Mathf.Abs(newSpeed));
    }

    public void jump()
    {
        if (grounded)
        {
            rigplayer.velocity = new Vector2(rigplayer.velocity.x, jumpheight);
            grounded = false;
            if (anim != null)
                anim.SetBool("Grounded", grounded);
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

        
    public IEnumerator shoot(bool is_normal_shot)
    {
        if (rangeAttackCooldown[0] >= rangeAttackCooldown[1])
        {
            if(anim!=null)
                anim.SetTrigger("Shot");
            while (!shotAnimationReady)
            {
                yield return null;
            }
            GameObject proj = PPS.getProjectile();
            shotAnimationReady = false;
            if (proj != null)
            {
                currentProjectile = proj.GetComponent<Projectile>();
                rangeAttackCooldown[0] = 0;
                if (is_normal_shot)
                    currentProjectile.set_shooting_type(Projectile.Shooting_Type.NORMAL);
                else
                    currentProjectile.set_shooting_type(Projectile.Shooting_Type.SPECIAL);
                currentProjectile.shoot(2.0f, facingRight);
            }
        }
    }

    //Methode auf die der Keyframe zugreift, um den Schuss zu ermöglichen
    public void setShotAnimationReady()
    {
        shotAnimationReady = true;
    }


//Waffe wechseln
    public void switchWeapon()
    {
        AmmoIconHandler iconHandler = GameObject.Find("HUD").GetComponent<AmmoIconHandler>();
        iconHandler.changeAmmo();
    }

    //Blocken
    public void block()
    {

    }

    public void unblock()
    {

    }
}
