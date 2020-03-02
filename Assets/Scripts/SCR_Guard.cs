using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SCR_Guard : MonoBehaviour
{
    public static event System.Action OnGuardHasSpottedPlayer;

    public LayerMask viewMask;
    public Light Spotlight;
    public float viewDistance;
    public float timeToSpotPlayer = .5f;
    public float damping = 6.0f;
    public int destPoint = 0;

    float viewAngle;
    float playerVisibleTimer;

    Color originalSpotlightColor;
    public Transform player;
    public NavMeshAgent agent;
    public Transform[] navPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = Spotlight.spotAngle;
        originalSpotlightColor = Spotlight.color;
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            Patrol();
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        Spotlight.color = Color.Lerp(originalSpotlightColor, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            if (OnGuardHasSpottedPlayer != null)
            {
                OnGuardHasSpottedPlayer();
                Chase();
                LookAtPlayer();
            }
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(player);
    }

    bool CanSeePlayer()
    {
        if(Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
    void Patrol()
    {
        if (navPoint.Length == 0)
            return;
        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }
}

