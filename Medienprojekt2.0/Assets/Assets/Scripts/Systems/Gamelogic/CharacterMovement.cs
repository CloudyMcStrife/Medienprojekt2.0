using UnityEngine;
using System.Collections;

//Beinhaltet alle Aktionen die der Spieler ausführen kann, dieses Script ist auch nur teil vom Spieler.

public class CharacterMovement : MonoBehaviour {
	Rigidbody2D rigplayer;
	Transform trans;
    Animator anim;

    //Für den Scream
    public bool unableToMove;
    public float speed = 4.0f;
    float scaling;
    public bool facingRight = true;
    public bool startingRight;

    public float[] rollCooldown = { 2.0f, 2.0f };
	public float rollDuration;
	public bool rolling;

    public bool grounded;
	public float jumpheight;
    public bool jumpReady;

	public LayerMask groundMask;
	public Transform groundCheck;
	public float groundCheckRadius;
	// Use this for initialization
	void Awake () {
        facingRight = startingRight;
		rigplayer = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		trans = (Transform)GetComponent (typeof(Transform));
        anim = (Animator)GetComponent(typeof(Animator));
        
        scaling = transform.localScale.x;
        jumpReady = false;
        unableToMove = false;
	}

	
	// Update is called once per frame

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundMask);
        if(anim!=null)
            anim.SetFloat("Speedy", rigplayer.velocity.y);
	}
	void Update () 
	{
		//cooldowns
		if (rollCooldown [0] < rollCooldown [1])
			rollCooldown [0] += Time.deltaTime;
		rolling = rollCooldown [0] < rollDuration;		
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
            anim.SetFloat("Speedx", Mathf.Abs(newSpeed));
    }

    public IEnumerator jump()
    {
        if (grounded)
        {
            /*if (anim != null)
            {
                anim.SetTrigger("Jump");
                while (!jumpReady)
                    yield return null;
            }*/
            if (jumpReady)
                yield return null;  

            rigplayer.velocity = new Vector2(rigplayer.velocity.x, jumpheight);
            jumpReady = grounded = false;
        }
    }

    public void setJumpReady()
    { jumpReady = true; }

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
}
