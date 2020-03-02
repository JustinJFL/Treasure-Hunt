using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class SCR_GuardSight : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent;
        public CharacterController character;

        public enum State
        {
            PATROL,
            CHASE,
            INVESTIGATE
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

        //Variables for Investigating
        private Vector3 investigateSpot;
        private float timer = 0;
        public float investigateWait = 10;

        //Variables for Sight
        public float heightMultiplier;
        public float sightDist = 10;

        void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            this.character = this.gameObject.GetComponent<CharacterController>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = SCR_GuardSight.State.PATROL;
            alive = true;
            heightMultiplier = 1.36f;
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
                    case State.INVESTIGATE:
                        Investigate();
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

        void Investigate()
        {
            timer += Time.deltaTime;
            RaycastHit hit;
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
            if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = SCR_GuardSight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier,(transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = SCR_GuardSight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier,(transform.forward - transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = SCR_GuardSight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero);
        }

        void OnTriggerEnter(Collider Coll)
        {
            if (Coll.tag == "Player")
            {
                state = SCR_GuardSight.State.CHASE;
                target = Coll.gameObject;
            }
        }
    }

