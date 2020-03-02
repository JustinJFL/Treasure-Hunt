using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class SCR_BasicAI : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent;
        public CharacterController character;

        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        private bool alive;

        //Variables For Patrolling
        public GameObject[] waypoints;
        private int waypointInd = 0;
        public float patrolSpeed = 0.5f;

        //Variables For Chasing
        public float chaseSpeed = 1f;
        public GameObject target;

        void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            this.character = this.gameObject.GetComponent<CharacterController>();
            

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = SCR_BasicAI.State.PATROL;
            alive = true;

            StartCoroutine("FSM");
        }

        IEnumerator FSM()
        {    

            while (alive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }
                yield return null;
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
            {
                waypointInd += 1;
                if (waypointInd > waypoints.Length)
                {
                    waypointInd = 0;
                }
            }
            else
            {
                
            }
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
        }

        void OnTriggerEnter(Collider Coll)
        {
            if (Coll.tag == "Player")
            {
                state = SCR_BasicAI.State.CHASE;
                target = Coll.gameObject;
            }
        }
    }
}