﻿using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    Animator anim;
    Collider2D coll;
    bool spawned = false;
    public bool debugRNG;

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

                inLeftHitBox = xDistance >= -2.3f && xDistance <= 0.3f;
                inRightHitBox = xDistance <= 2.3f && xDistance >= -.3f;


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

    void Spawned()
    {
        spawned = true;
        coll.enabled = true;
    }

    void FightingDecisions()
    {
        //Lauf dem Spieler son bisschen hinterher
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

            //Aktion ready
            if(actionCooldown[0] >= actionCooldown[1])
            {
                actionRunning = true;
                actionCooldown[0] = 0.0f;
                getProbabilities();

                float rng = Random.value;
                if (debugRNG)
                {
                    Debug.Log("###################################");
                    Debug.Log("Right: " + inRightHitBox + " Left: " + inLeftHitBox);
                    Debug.Log("RNG :" + rng);
                    Debug.Log("Left Prob:" + leftHandProb);
                    Debug.Log("Right Prob:" + rightHandProb);
                    Debug.Log("Scream Prob:" + screamProb);
                }

                if (!inHitBox)
                {
                    Scream();
                }
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


    void LeftHandHit()
    {
        //Variablen für den Code, welche attacke benutzt werden soll
        if(debugRNG)
            Debug.Log("Result: Left Hit");
        timesSinceRightHand++;
        timesSinceScream++;
        timesSinceLeftHand = 1;

        //Real Stuff
        idleStateExecuted = false;
        anim.SetTrigger("LeftHandAttack");
    }

    void LeftHandDamage()
    {
        if (inLeftHitBox && correctHeight)
            playerHealth.lowerHealth(leftDamage);
    }

    void RightHandHit()
    {
        //Variablen für den Code, welche attacke benutzt werden soll
        if (debugRNG)
            Debug.Log("Result: Right");
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

    void Scream()
    {
        //Variablen für den Code, welche attacke benutzt werden soll
        if (debugRNG)
            Debug.Log("Result: Scream");
        timesSinceLeftHand++;
        timesSinceRightHand++;
        timesSinceScream = 0;
        actionCooldown[0] += 2.0f;

        //Real Stuff
        idleStateExecuted = false;
        anim.SetTrigger("Scream");
    }

    public void screamReady()
    {
        screamTime[0] = 0.0f;
        StartCoroutine(screamAction());
    }

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