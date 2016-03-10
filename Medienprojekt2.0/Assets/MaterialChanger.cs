using UnityEngine;
using System.Collections;

public class MaterialChanger : StateMachineBehaviour {

    Material[] materials = new Material[11];

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        SpriteRenderer renderer = animator.gameObject.GetComponent<SpriteRenderer>();
        int index = -1;
        string path = "";
        int tmp = 0;
        if (stateInfo.IsName("Idle"))
        {
            if (materials[0] != null) { index = 0; }
            else{ path = "Materials/idle";}
        } else if (stateInfo.IsName("Block"))
        {
            if (materials[1] != null) { index = 1; }
            else { path = "Materials/shield_prep"; tmp = 1; }
        } else if (stateInfo.IsName("Block_Back"))
        {
            if (materials[2] != null) { index = 2; }
            else { path = "Materials/shield_prep"; tmp = 2; }
        }
        else if (stateInfo.IsName("Jumping"))
        {
            if (materials[3] != null) { index = 3; }
            else { path = "Materials/jump"; tmp = 3; }
        }
        else if (stateInfo.IsName("Walking"))
        {
            if (materials[4] != null) { index = 4; }
            else { path = "Materials/run"; tmp = 4; }
        }
        else if (stateInfo.IsName("RunningAttack"))
        {
            if (materials[5] != null) { index = 5; }
            else { path = "Materials/run_attack"; tmp = 5; }
        }
        else if (stateInfo.IsName("IdleAttack"))
        {
            if (materials[6] != null) { index = 6; }
            else { path = "Materials/idle_attack"; tmp = 6; }
        }
        else if (stateInfo.IsName("MC_IdleSwordAttack"))
        {
            if (materials[7] != null) { index = 7; }
            else { path = "Materials/idle_sword"; tmp = 7; }
        }
        else if (stateInfo.IsName("MC_SwordAttack2"))
        {
            if (materials[8] != null) { index = 8; }
            else { path = "Materials/sw2"; tmp = 8; }
        }
        else if (stateInfo.IsName("MC_SwordAttack3"))
        {
            if (materials[9] != null) { index = 9; }
            else { path = "Materials/sw3"; tmp = 9; }
        } 
        else if (stateInfo.IsName("MC_SwordAttack4"))
        {
            if (materials[10] != null) { index = 10; }
            else { path = "Materials/sw4";tmp = 10; }
        }

        if(index != -1)
        {
            renderer.material = materials[index];
        }
        else
        {
            Material m = Resources.Load<Material>(path);
            Debug.Log("Loaded " + path);
            materials[tmp] = m;
            
            renderer.material = m;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
