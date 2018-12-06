using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    private Animator anim;
    private GameObject player;
    private Ray ray;
    private RaycastHit hit;
    private Transform[] waypoints;
    private Vector3 direction;
    private float maxDistance = 6.0f;
    private float currentDistance, distanceFromTarget;
    private int currentTarget;

    public Transform pointA, pointB;
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        waypoints = new Transform[2] { pointA, pointB };
        currentTarget = 0;
        //navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }

    private void FixedUpdate()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        anim.SetFloat("distance", currentDistance);

        direction = player.transform.position - transform.position;
        ray = new Ray(transform.position, direction);
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject == player)
                anim.SetBool("playerVisable", true);
            else
                anim.SetBool("playerVisable", false);
        } else
        {
            anim.SetBool("playerVisable", false);
        }

        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        anim.SetFloat("distanceFromWaypoint", distanceFromTarget);
    }

    public void SetNextPoint()
    {
        switch (currentTarget)
        {
            case 0: currentTarget = 1;
                break;
            case 1: currentTarget = 0;
                break;
        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
}


