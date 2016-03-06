using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    Animator anim;
    bool spawned = false;

    // Use this for initialization
    void Start()
    {
        anim = (Animator)GetComponent(typeof(Animator));
    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //who won? who's next? You decide! Epic Battles of history :D
        FightingDecisions();
    }


    //Called by Spawn Trigger to spawn the boss
    public void Spawn()
    {
        spawned = true;
        anim.SetTrigger("Spawn");
    }

    void FightingDecisions()
    {

    }
}