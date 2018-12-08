using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWanderController : MonoBehaviour
{
    private Vector3 target;
    private NavMeshAgent navMeshAgent;

    public float maxDistance = 5.0f;
    public float targetPositionTolerance = 3.0f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target, targetPositionTolerance);
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetNextPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(target, transform.position) <= targetPositionTolerance)
        {
            GetNextPosition();
        }

        navMeshAgent.SetDestination(target);
    }

    private void GetNextPosition()
    {
        target = Random.onUnitSphere * maxDistance;
        target.x += transform.position.x;
        target.z += transform.position.z;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, maxDistance, 1))
        {
            target = hit.position;
        }
        else
        {
            GetNextPosition();
        }
    }
}
