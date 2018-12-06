using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowerController : MonoBehaviour {

    [SerializeField]
    private Path path;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float mass;
    [SerializeField]
    private bool isLooping;

    private float currentSpeed;
    private int currentPathIndex = 0;
    private Vector3 targetPos;
    private Vector3 direction;
    private Vector3 targetDirection;

	// Use this for initialization
	void Start () {
        direction = transform.forward;
        targetPos = path.GetPoint(currentPathIndex);
	}
	
	// Update is called once per frame
	void Update () {
        if (path == null)
            return;

        currentSpeed = speed * Time.deltaTime;

        if (TargetReached())
        {
            if (!SetNextTarget())
                return; 
        }

        direction += Steer(targetPos);
        transform.position += direction;

        transform.rotation = Quaternion.LookRotation(direction);
	}

    private bool TargetReached()
    {
        return (Vector3.Distance(transform.position, targetPos) < path.radius);
    }

    private bool SetNextTarget()
    {
        bool success = false;
        if (currentPathIndex < path.PathLength - 1)
        {
            currentPathIndex++;
            success = true;
        }
        else
        {
            if (isLooping)
            {
                currentPathIndex = 0;
                success = true;
            } else
            {
                success = false;
            }
        }
        targetPos = path.GetPoint(currentPathIndex);
        return success;
    }

    public Vector3 Steer(Vector3 target)
    {
        targetDirection = (target - transform.position);
        targetDirection.Normalize();
        targetDirection *= currentSpeed;
        Vector3 steeringForce = targetDirection - direction;
        Vector3 accel = steeringForce / mass;
        return accel;
    }


}
