using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public enum AIState { Wander, Chase, Attack }
    public AIState state = AIState.Wander;
    public bool playerVisable = false;

    public float maxChaseDistance = 10.0f;
    public float maxAttackDistance = 5.0f;

    private Transform playerTransform;
    private float distanceToPlayer;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > maxChaseDistance && !playerVisable)
        {
            state = AIState.Wander;
        }

        if (playerVisable && distanceToPlayer < maxChaseDistance)
        {
            state = AIState.Chase;
        }

        if (playerVisable && distanceToPlayer < maxAttackDistance)
        {
            state = AIState.Attack;
        }

    }

}


