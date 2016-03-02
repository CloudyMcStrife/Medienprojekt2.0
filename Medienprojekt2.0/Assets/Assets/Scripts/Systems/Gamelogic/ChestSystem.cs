using UnityEngine;
using System.Collections;
public class ChestSystem : MonoBehaviour {

    Animator anim;
    InputSystem playerInput;
    // Use this for initialization
    void Start () {
        anim = (Animator)this.GetComponent(typeof(Animator));
        playerInput = (InputSystem) GameObject.Find("Player").GetComponent(typeof(InputSystem));
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D()
    {

        anim.SetBool("ChestOpened", true);
    }
}
