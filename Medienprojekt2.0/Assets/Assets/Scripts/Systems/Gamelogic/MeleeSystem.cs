using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    AttributeComponent attributes;
    Animator anim;
    bool blocking;

	// Use this for initialization
	void Awake () {
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
        anim = (Animator)GetComponent(typeof(Animator));
	}
	
	// Update is called once per frame
	void Update () {
        if (attributes.stamina == 0)
            unblock();
        Debug.Log(blocking);
	}

    public void setBlockReady()
    {
        blocking = true;
    }

    public void block()
    {
        anim.SetBool("Blocking", true);
    }

    public void unblock()
    {
        blocking = false;
        anim.SetBool("Blocking", false);
    }
}
