using UnityEngine;
using System.Collections;
public class ChestSystem : MonoBehaviour {

    Animator anim;
    public GameObject[] possibleDrops;
    // Use this for initialization
    void Start () {
        anim = (Animator)this.GetComponent(typeof(Animator));
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay2D()
    {
        if(!anim.GetBool("ChestOpened"))
        {
            if(Input.GetKeyDown("e"))
                anim.SetBool("ChestOpened", true);
        }
    }


    //Wird von der Animation aufgerufen und erschafft zufällig eines der eingestellten Objekte
    void ANIM_DropKeyframe()
    {
        int randomIndex = (int)(Random.value * possibleDrops.Length);
        GameObject dropped = (GameObject)Instantiate((GameObject)possibleDrops[randomIndex], this.transform.position, this.transform.localRotation);
    }
}
