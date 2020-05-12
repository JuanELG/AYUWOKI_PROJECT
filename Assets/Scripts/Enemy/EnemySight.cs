using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    [SerializeField]
    private float fieldOfViewAngle = 110;
    public bool playerInSight;
    public Vector3 personalLastSighting;


    private NavMeshAgent navMesh;
    private SphereCollider myCollider;
    private LastPlayerSighting lastPlayerSighting;
    private GameObject player;
    private Vector3 previousSighting;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        myCollider = GetComponent<SphereCollider>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag("Player");

        personalLastSighting = lastPlayerSighting.position;
        previousSighting = lastPlayerSighting.resetPosition;
    }

    private void Update()
    {
        if(lastPlayerSighting.position != previousSighting)
        {
            personalLastSighting = lastPlayerSighting.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                Debug.DrawLine(transform.position + transform.up, direction.normalized, Color.red);
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, myCollider.radius))
                {
                    if(hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        lastPlayerSighting.position = player.transform.position;
                    }
                }
            }
            if(CalculatePathLength(player.transform.position) <= myCollider.radius)
            {
                personalLastSighting = player.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;
        }
    }

    private float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (navMesh.enabled)
        {
            navMesh.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for(int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for(int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
