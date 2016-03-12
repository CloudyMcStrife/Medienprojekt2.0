using UnityEngine;
using System.Collections;

/* Der Geskriptete Ablauf des Bosskampfes.
Der Boss spawnt, wenn die Zeit abläuft, die beginnt, nachdem die Spawnzone betreten wurde*/

public class BossScript : MonoBehaviour
{
    Animator anim;
    Collider2D coll;
    bool spawned = false;

    float xDistance;
    
    float desiredXPosition;
    bool inHitBox;
    GameObject target;
    CharacterMovement playerMovement;
    HealthSystem playerHealth;

    public float speed;
    
    public float leftDamage;
    public bool inLeftHitBox;
    int timesSinceLeftHand;
    float leftHandProb;

    public float rightDamage;
    public bool inRightHitBox;
    int timesSinceRightHand;
    float rightHandProb;


    int timesSinceScream;
    public float[] screamTime = { 3.0f, 3.0f };
    float screamProb;
    bool correctHeight;
    public float [] actionCooldown = {5.0f, 5.0f};
    bool idleStateExecuted;
    bool actionRunning;

    // Use this for initialization
    void Start()
    {
        coll = (Collider2D)GetComponent(typeof(Collider2D));
        coll.enabled = false;
        playerMovement = (CharacterMovement)GameObject.Find("Player").GetComponent(typeof(CharacterMovement));
        playerHealth = (HealthSystem)GameObject.Find("Player").GetComponent(typeof(HealthSystem));
        anim = (Animator)GetComponent(typeof(Animator));
        timesSinceScream = timesSinceRightHand = timesSinceLeftHand = 1;
    }

    void FixedUpdate()
    {
        if(spawned)
        {
            //distance check
            if(target != null)
            {
                xDistance = target.transform.position.x - transform.position.x;

                //Checks ob der SPieler sich in Hitboxen befindet
                inLeftHitBox = xDistance >= -2.3f && xDistance <= 0.3f;
                inRightHitBox = xDistance <= 2.3f && xDistance >= -.3f;



                //Kombinierter  Check
                inHitBox = inLeftHitBox || inRightHitBox;

                


                //Magicnumber, wenn der Spieler drüber ist
                correctHeight = target.transform.position.y < 49.7f && target.transform.position.y >= 47.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
        {
            if (actionCooldown[0] < actionCooldown[1])
                actionCooldown[0] += Time.deltaTime;

            if (screamTime[0] < screamTime[1])
                screamTime[0] += Time.deltaTime;

            //Klon vs Player
            if (GameObject.Find("Klon"))
                target = (GameObject)GameObject.Find("Klon");

            else if (GameObject.Find("Player"))
                target = (GameObject)GameObject.FindWithTag("Player");

            else
                target = null;



            FightingDecisions();
        }
    }

    //Called by Spawn Trigger to spawn the boss
    public void Spawn()
    {
        anim.SetTrigger("Spawn");
    }


    /*<summary>
    Methode die aufgerufen wird, wenn die Spawnanimation abgeschlossen ist
    </summary>
    */
    void Spawned()
    {
        spawned = true;
        coll.enabled = true;
    }


    /*
    <summary>
    Die Methode die über die nächste Kampfaktion des Boss' entscheidet.
    Es gibt 3 Attacken: Schlagen mit der Linken Hand, Schlagen mit der rechten Hand, schreien um den Spieler erstarren zulassen.
    Am Anfang ist jede Attacke gleich wahrscheinlich. Jedoch merkt sich der Boss welche Attacke wie lange nicht mehr ausgeführt wurde, und erhöht deren Wahrscheinlichkeit entsprechend.
    Am Anfang somit 1/3 Chance ausgeführt zu werden.  Wird nun der Schrei gewählt, haben die linke und rechte Attacke 50%, der Schrei wird nicht nochmal durchgeführt
    </summary>
    */
    void FightingDecisions()
    {
        //Ist der Spieler nicht in Reichweite, verfolge ihn, bis auf eine Entfernung von 1
        if(!inHitBox && !actionRunning )
        {
            desiredXPosition = target.transform.position.x;
            if(Mathf.Abs(transform.position.x - desiredXPosition) > 1.0f)
            {
                Vector3 newPos = transform.position;
                if (xDistance < 0)
                    newPos.x -= speed * Time.deltaTime;
                else
                    newPos.x += speed * Time.deltaTime;
                transform.position = newPos;
            }
            return;
        }
        else
        {

            //Wenn der Cooldown abgelaufen ist, führe die nächste Aktion durch.
            if(actionCooldown[0] >= actionCooldown[1])
            {
                actionRunning = true;
                actionCooldown[0] = 0.0f;
                
                //Methode die die Wahrscheinlichkeiten der einzelnen Aktionen berechnet
                getProbabilities();

                float rng = Random.value;


                //Wenn der Spieler nicht erreichbar ist, schreie (hält diesen fest)
                if (!inHitBox)
                {
                    Scream();
                }


                /*Wahrscheinlichkeits intervalle für die einzelnen Aktionen.
                0 <= rng <= 1
                */
                else if (inLeftHitBox && inRightHitBox)
                {
                    if (rng <= screamProb)
                        Scream();
                    else if (rng > screamProb && rng <= (screamProb + leftHandProb))
                    {
                        LeftHandHit();
                    }
                    else if (rng > (screamProb + leftHandProb))
                    {
                        RightHandHit();
                    }
                }


                else if (inLeftHitBox)
                {
                    if (rng <= screamProb)
                        Scream();
                    else 
                        LeftHandHit();
                }



                else if (inRightHitBox)
                {
                    if (rng <= screamProb)
                        Scream();
                    else
                        RightHandHit();
                }


            }
        }
    }

    //Die Zähler für die letzten  Aktionen werden erhöht, der von der ausgeführten Aktion zurückgesetzt.
    //Dann wird die Aktion durchgeführt
    void LeftHandHit()
    {
        //Variablen für den Code, welche attacke benutzt werden soll
        timesSinceRightHand++;
        timesSinceScream++;
        timesSinceLeftHand = 1;

        //Real Stuff
        idleStateExecuted = false;
        anim.SetTrigger("LeftHandAttack");
    }


    //Methode die im entsprechenden Keyfram aufgerufen wird, um den Schaden auszuführen
    void LeftHandDamage()
    {
        if (inLeftHitBox && correctHeight)
            playerHealth.lowerHealth(leftDamage);
    }

    void RightHandHit()
    {
        //Variablen für den Code, welche attacke benutzt werden soll
        timesSinceScream++;
        timesSinceLeftHand++;
        timesSinceRightHand = 1;

        //Real Stuff
        idleStateExecuted = false;
        anim.SetTrigger("RightHandAttack");
    }
    void RightHandDamage()
    {
        if (inRightHitBox && correctHeight)
            playerHealth.lowerHealth(rightDamage);
    }


    //Startet die Schreianimation
    void Scream()
    {
        //Variablen für den Code, welche attacke benutzt werden soll
        timesSinceLeftHand++;
        timesSinceRightHand++;
        timesSinceScream = 0;
        actionCooldown[0] += 2.0f;

        //Real Stuff
        idleStateExecuted = false;
        anim.SetTrigger("Scream");
    }

    //Wird von der Animation im entspr. Keyframe aufgerufen.
    //Startet den Schreieffekt
    public void screamReady()
    {
        screamTime[0] = 0.0f;
        StartCoroutine(screamAction());
    }

    //Solane die Screamtime läuft, kann sich der Spieler nicht bewegen
    //Lässt Spieler erstarren
    public IEnumerator screamAction()
    {
        playerMovement.unableToMove = true;
        while (screamTime[0] < screamTime[1])
            yield return null;

        playerMovement.unableToMove = false;
    }

    void getProbabilities()
    {
        int options = timesSinceScream;
        if (inLeftHitBox)
        { 
            options += timesSinceLeftHand;
        }
        if(inRightHitBox)
        {
            options += timesSinceRightHand;
        }
        screamProb = (float)timesSinceScream / (float)options;

        leftHandProb = inLeftHitBox ? (float)timesSinceLeftHand / (float)options : 0.0f;
        rightHandProb = inRightHitBox ? (float)timesSinceRightHand / (float)options : 0.0f;
    }

    public void idleState()
    {
        if(!idleStateExecuted)
        { 
            idleStateExecuted = true;
            actionRunning = false;
        }
    }
}