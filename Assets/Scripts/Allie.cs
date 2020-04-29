using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Allie : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Rigidbody myRB;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private int currentWaypoint;
    [SerializeField]
    private float speed, stopDistance, pauseTimer;
    [SerializeField]
    private float currentTimer;

    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        myRB = GetComponent<Rigidbody>();

        myRB.freezeRotation = true;

        target = waypoints[currentWaypoint];
        currentTimer = pauseTimer;
    }

    private void Update()
    {
        MovementToWaypoint();
    }    

    private void MovementToWaypoint()
    {
        navMesh.acceleration = speed;
        navMesh.stoppingDistance = stopDistance;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > stopDistance && waypoints.Length > 0)
        {
            target = waypoints[currentWaypoint];
        }
        else if (distance <= stopDistance && waypoints.Length > 0)
        {
            if (currentTimer > 0)
            {
                currentTimer -= 0.1f;
            }
            if (currentTimer <= 0)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
                target = waypoints[currentWaypoint];
                currentTimer = pauseTimer;
            }
        }
        navMesh.SetDestination(target.position);
    }
}
