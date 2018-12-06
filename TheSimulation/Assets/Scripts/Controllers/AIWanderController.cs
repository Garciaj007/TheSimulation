using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWanderController : MonoBehaviour
{
    private Vector3 target;

    public float maxDistance = 5.0f;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float targetPositionTolerance = 3.0f;

    private void OnDrawGizmos()
    {
        if (target == null)
            return;

        Gizmos.DrawSphere(target, targetPositionTolerance);
    }

    private void Start()
    {
        GetNextPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(target, transform.position) <= targetPositionTolerance)
        {
            GetNextPosition();
        }

        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
    }

    private void GetNextPosition()
    {
        target = Random.insideUnitSphere * maxDistance;
        target += transform.position;

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
