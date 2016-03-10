using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    Animator anim;
    bool spawned = false;

    float xDistance;
    float desiredXPosition;
    bool inHitBox;
    GameObject target;

    public float speed;

    public bool inLeftHitBox;
    int timesSinceLeftHand;

    public bool inRightHitBox;
    int timesSinceRightHand;


    int timesSinceScream;

    float [] actionCooldown = {5.0f, 5.0f};

    // Use this for initialization
    void Start()
    {
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
                inHitBox = Mathf.Abs(xDistance) <= 2.0f;
                Debug.Log(xDistance);

                inLeftHitBox = xDistance >= -2.3f && xDistance <= 0.3f;
                inRightHitBox = xDistance <= 2.3f && xDistance >= -.3f;
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
    }

    void FightingDecisions()
    {
        //Lauf dem Spieler son bisschen hinterher
        if(!inHitBox)
        {
            desiredXPosition = target.transform.position.x;
            if(Mathf.Abs(transform.position.x - desiredXPosition) > 1.0f)
            {
                Vector3 newPos = transform.position;
                if (xDistance < 0)
                    newPos.x += speed * Time.deltaTime;
                else
                    newPos.x -= speed * Time.deltaTime;
                transform.position = newPos;
            }
            return;
        }
        else
        {

            //Aktion ready
            if(actionCooldown[0] >= actionCooldown[1])
            {
                actionCooldown[0] = 0.0f;
                int options = timesSinceScream;
                int decision = -1;
                if (inLeftHitBox)
                    options+= timesSinceLeftHand;
                if (inRightHitBox)
                    options+= timesSinceRightHand;

                //Nur Schreien möglich
                if(! (inLeftHitBox || inRightHitBox) )
                {
                    Scream();
                }
                else if(inLeftHitBox && inRightHitBox)
                {
                    decision = Random.Range(0, options);
                }
                else if(inRightHitBox)
                {
                    decision = Random.Range(0, options);
                }
                else if(inLeftHitBox)
                {
                    decision = Random.Range(0, options);
                }

            }
        }
    }

    void LeftHandHit()
    {
        timesSinceRightHand++;
        timesSinceScream++;
        timesSinceLeftHand = 1;


    }

    void RightHandHit()
    {
        timesSinceScream++;
        timesSinceLeftHand++;
        timesSinceRightHand = 1;


    }

    void Scream()
    {
        timesSinceLeftHand++;
        timesSinceRightHand++;
        timesSinceScream = 1;


    }
}