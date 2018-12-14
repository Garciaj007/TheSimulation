using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIController))]
public class AIWanderController : MonoBehaviour
{
    private Vector3 target;
    private NavMeshAgent navMeshAgent;
    private AIController controller;

    public float maxDistance = 5.0f;
    public float targetPositionTolerance = 3.0f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target, targetPositionTolerance);
    }

    private void Start()
    {
        controller = GetComponent<AIController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetNextPosition();
    }

    private void Update()
    {
        if (controller.state == AIController.AIState.Wander)
        {
            if (Vector3.Distance(target, transform.position) <= targetPositionTolerance)
                GetNextPosition();

            navMeshAgent.SetDestination(target);
        }
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
