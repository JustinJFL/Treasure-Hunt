using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Patrol : Guard_Base_FSM
{
    GameObject Guard;
    GameObject[] waypoints;
    int currentWP;

    void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Guard = animator.gameObject;
        currentWP = 0;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waypoints.Length == 0) return;
        if (Vector3.Distance(waypoints[currentWP].transform.position, Guard.transform.position) < accuracy)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }
        //rotates towards target
        var direction = waypoints[currentWP].transform.position - Guard.transform.position;
        Guard.transform.rotation = Quaternion.Slerp(Guard.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        Guard.transform.Translate(0, 0, Time.deltaTime * speed);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        
    }


}
