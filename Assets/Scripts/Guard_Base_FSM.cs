using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Base_FSM : StateMachineBehaviour
{
    public GameObject Guard;
    public GameObject Player;
    public float speed = 20.0f;
    public float rotSpeed = 5.0f;
    public float accuracy = 20.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Guard = animator.gameObject;
        Player = Guard.GetComponent<SCR_GuardAI>().GetPlayer();
    }
}
