using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    private int destinationPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private float remainingDistance = 0.5F;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
    }
    
    void GoToNextPoint()
    {
        if(points.Length == 0)
        {
            Debug.LogError("You must set up at least one destination");
            enabled = false;
            return;
        }
        agent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

    private void Update()
        //Parse if the guard is currently on route to a point and if not initiate a path
    {
        if(!agent.pathPending && agent.remainingDistance < remainingDistance)
        {
            GoToNextPoint();
        }
    }
}
