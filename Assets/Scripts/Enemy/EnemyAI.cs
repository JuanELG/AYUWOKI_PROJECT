using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    private EnemySight enemySight;
    private NavMeshAgent navMesh;
    private Transform player;
    private LastPlayerSighting lastPlayerSighting;
    private float chaseTimer;
    private float patrolTimer;
    private int currentWayPointIndex;

    private void Awake()
    {
        enemySight = GetComponent<EnemySight>();
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
    }

    private void Update()
    {
        if (enemySight.playerInSight)
        {
            //atacar
        }
        else if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    private void Attack()
    {
        navMesh.isStopped = true;
    }

    private void Chasing()
    {
        Vector3 sightingDeltaPosition = enemySight.personalLastSighting - transform.position;
        if(sightingDeltaPosition.sqrMagnitude > 4f)
        {
            navMesh.destination = enemySight.personalLastSighting;
        }
        navMesh.speed = chaseSpeed;

        if(navMesh.remainingDistance < navMesh.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if(chaseTimer > chaseWaitTime)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
        {
            chaseTimer = 0f;
        }
    }

    private void Patrolling()
    {
        navMesh.speed = patrolSpeed;
        if(navMesh.destination == lastPlayerSighting.resetPosition || navMesh.remainingDistance < navMesh.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if(patrolTimer >= patrolWaitTime)
            {
                if(currentWayPointIndex == (patrolWayPoints.Length - 1))
                {
                    currentWayPointIndex = 0;
                }
                else
                {
                    currentWayPointIndex++;
                }
                patrolTimer = 0f;
            }
        }
        else
        {
            patrolTimer = 0f;
        }
        navMesh.destination = patrolWayPoints[currentWayPointIndex].position;
    }
}
