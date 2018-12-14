using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(AIController))]
public class AIChaseController : MonoBehaviour {

    private Transform playerTransform;
    private NavMeshAgent agent;
    private AIController controller;
    private float distanceToPlayer;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<AIController>();
	}

    public void Update()
    {
        if (controller.state == AIController.AIState.Chase)
        {
            transform.LookAt(playerTransform.position, transform.up);

            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > controller.maxAttackDistance && distanceToPlayer <= controller.maxChaseDistance)
            {
                NavMeshHit hit;
                if(NavMesh.SamplePosition(Vector3.Lerp(playerTransform.position, transform.position, 0.5f), out hit, 1000.0f, 1))
                {
                    agent.SetDestination(hit.position);
                }
            }
        }
    }
}
