using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {
    /* Eine grundlegende KI die nach verschiedenen Checks das Gegnerverhalten auswählt.
    Nach Auswahl eines Verhaltens wird dann die entsprechende Component angesprochen, sodass die AI unabhängig von Bewegung /Angriffen o.Ä. bleibt*/


    CharacterMovement movement;
    RangedSystem rangeSys;
    Animator anim;

    Rigidbody2D rigplayer;
    Rigidbody2D rigenemy;

    //Eine Visionkomponent die zum überprüfen des Sichtkontakts da ist
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

    
    /*Da die KI den Gegner zum umdrehen bewgen soll, wenn sie gegen eine Wand läuft, oder sich vor einem Abgrund befindet,
    wird ein Check mit Hilfe eines kleinen Gameobjects durchgeführt, ob sich vor dem Gegner eine Wand befindet, oder der Boden abrupt endet*/
    public LayerMask wallMask;
    public Transform wallCheck;
    public float wallCheckRadius;
    bool hittingWall;

    public LayerMask gapMask;
    public Transform gapCheck;
    public float gapCheckRadius;
    bool onAnEdge;

    // Use this for initialization
    void Awake()
    {
        movement = (CharacterMovement)gameObject.GetComponent(typeof(CharacterMovement));
        walkingRight = false;
        rigplayer = (Rigidbody2D)GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
        rigenemy = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        vision = (EnemyVision)GetComponent(typeof(EnemyVision));
        rangeSys = (RangedSystem)GetComponent(typeof(RangedSystem));
        anim = (Animator)GetComponent(typeof(Animator));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rigplayer != null)
        {
            //Distanzen zum Spieler finden
            distancex = rigplayer.position.x - rigenemy.position.x;
            distancey = rigplayer.position.y - rigenemy.position.y;
        }
        else
        {
            distancex = distancey = 0;
        }

        //Checkt ob sich der Gegner vor einer Wand befindet, oder der Boden endet (=Abgrund)
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallMask);
        onAnEdge = !Physics2D.OverlapCircle(gapCheck.position, gapCheckRadius, gapMask);
    }


    void Update()
    {

        //Rudimentäre Methode um Gegner den Klon kurzzeitig als Primärziel auszuwählen
        if (GameObject.Find("Klon"))
        {
            rigplayer = (Rigidbody2D)GameObject.Find("Klon").GetComponent(typeof(Rigidbody2D));
        }
        else
        {
            rigplayer = (Rigidbody2D)GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
        }
                
        //prüft ob EnemyEntity links oder rechts in minimumDistance (=Angriffsreichweite) ist;
        inAttackRangex = (distancex <= minimumDistancex && distancex > 0) || (distancex >= -minimumDistancex && distancex < 0);
        inAttackRangey = (distancey <= minimumDistancey && distancey > 0) || (distancey >= -minimumDistancey && distancey < 0);


        //Während eine Angriffanimation läuft, soll nicht angegriffen werden
        if (anim.GetBool("AttackInProgress"))
            return;


        //Wenn in Angriffsreichweite, und der Spieler sichtbar ist, bleibe stehen beginne anzugreifen
        //Ruft die Angriffsmethode des RangedSystems auf
        if ((inAttackRangex && inAttackRangey) && vision.playerVisible)
        {
            movement.move(0.0f);
            anim.SetBool("AttackInProgress", true);
            StartCoroutine(rangeSys.shoot(true));
        }

        //Wenn sich Spieler nicht in Reichweite zum Angreifen befindet, aber Sichtkontakt besteht, nimm Verfolgung auf
        //Ruft die Bewegungsmethode des Movementssystem auf.
        if (!inAttackRangex && inAttackRangey && vision.playerVisible)
        {
            if (movement.grounded)
            {
                if (walkingRight)
                    movement.move(1.0f);
                else
                    movement.move(-1.0f);
            }
            else
                movement.move(0.0f);
        }


        //Wenn das Ziel nicht sichtbar ist, soll patrolliert werden
        if (!vision.playerVisible)
        {
            //Wenn wir auf dem Boden befinden, beweg dich, ansonsten bleib stehen
            if (movement.grounded)
            {
                //Befinden wir uns vor einer Wand oder einem Abgrund, drehe um.
                if (hittingWall || onAnEdge)
                    walkingRight = !walkingRight;

                if (walkingRight)
                {
                    movement.move(1.0f);
                }
                else
                {
                    movement.move(-1.0f);
                }
            }
            else
                movement.move(0.0f);
        }
    }
}
