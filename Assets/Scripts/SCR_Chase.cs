using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Chase : Guard_Base_FSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var direction = Player.transform.position - Guard.transform.position;
        Guard.transform.rotation = Quaternion.Slerp(Guard.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        Guard.transform.Translate(0, 0, Time.deltaTime * speed);

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
       
    }

    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        // Implement code that processes and affects root motion
    //}

    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        // Implement code that sets up animation IK (inverse kinematics)
    //}
}
