using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

    CharacterMovement actions;

    Rigidbody2D rigplayer;
    Rigidbody2D rigenemy;
    EnemyVision vision;

    //Movement
    public bool walkingRight;


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

    // Use this for initialization
    void Awake()
    {
        actions = (CharacterMovement)gameObject.GetComponent(typeof(CharacterMovement));
        walkingRight = false;
        rigplayer = (Rigidbody2D)GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
        rigenemy = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        vision = (EnemyVision)GetComponent(typeof(EnemyVision));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distancex = rigplayer.position.x - rigenemy.position.x;
        distancey = rigplayer.position.y - rigenemy.position.y;
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallMask);
        onAnEdge = !Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
    }


    void Update()
    {
        //prüft ob EnemyEntity links oder rechts in minimumDistance (=Angriffsreichweite) ist;
        inAttackRangex = (distancex <= minimumDistancex && distancex > 0) || (distancex >= -minimumDistancex && distancex < 0);
        inAttackRangey = (distancey <= minimumDistancey && distancey > 0) || (distancey >= -minimumDistancey && distancey < 0);

        //Begin Chasing
        if (!inAttackRangex && vision.playerVisible)
        {
            if (actions.grounded)
            {
                if (walkingRight)
                    actions.move(1.0f);
                else
                    actions.move(-1.0f);
            }
            else
                actions.stopMovement();
        }

        //Should be able to attack now, if cooldowns are up
        if ((inAttackRangex && inAttackRangey) && vision.playerVisible)
        {
            actions.stopMovement();
            actions.shoot(true);
        }


        //Patrolling behaviour if not attacking or chasing
        if (!vision.playerVisible)
        {
            if (actions.grounded)
            {
                if (hittingWall || onAnEdge)
                    walkingRight = !walkingRight;
                if (walkingRight)
                {
                    actions.move(1.0f);
                }
                else
                {
                    actions.move(-1.0f);
                }
            }
            else
                actions.stopMovement();
        }
    }
}
