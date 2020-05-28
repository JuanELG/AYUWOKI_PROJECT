using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllieController : MonoBehaviour
{
    [SerializeField]
    private float lookRadius = 5f;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject[] wayPoints;
    private NavMeshAgent agent;
    [SerializeField]
    private bool following;
    [SerializeField]
    private int currentWaypointIndex = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(wayPoints.Length > 0)
            target = wayPoints[0].transform;
        following = false;
    }

    private void Update()
    {
        if (!following)
        {
            GoToWayPoint();
        }
        else
        {
            FollowPlayer();
        }
    }

    private void GoToWayPoint()
    {
        if (target == null)
            return;
        float distance = Vector3.Distance(target.position, transform.position);
        FaceTarget();
        if(distance <= agent.stoppingDistance)
        {
            target = wayPoints[((currentWaypointIndex + 1) % wayPoints.Length)].transform;
            currentWaypointIndex++;
        }
        agent.SetDestination(target.position);
    }

    private void FollowPlayer()
    {
        if (target == null)
            return;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            target = other.transform;
            following = true;
        }
    }
}
