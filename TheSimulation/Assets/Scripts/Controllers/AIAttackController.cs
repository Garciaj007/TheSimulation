using UnityEngine;

[RequireComponent(typeof(AIController))]
public class AIAttackController : MonoBehaviour {

    public float timeBetweenShots;
    public GameObject bullet;
    public Transform instancePosition;

    private AIController controller;
    private EntityController entityToAttack;
    private RaycastHit rayInfo;
    private Transform playerTransform;
    private Timer bulletSpawnTimer;
    private Ray ray;

    protected virtual void Start()
    {
        controller = GetComponent<AIController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        bulletSpawnTimer = gameObject.AddComponent<Timer>();
        bulletSpawnTimer.Duration = timeBetweenShots;
        bulletSpawnTimer.TimerDone += Attack;
    }

    protected virtual void Update()
    {
        if (controller.state == AIController.AIState.Attack)
        {
            transform.LookAt(playerTransform.position, transform.up);
            bulletSpawnTimer.Begin();
        }
        else
        {
            bulletSpawnTimer.Pause();
        }        
    }

    public virtual void Attack()
    {
        GameObject b = Instantiate(bullet, instancePosition.position, Quaternion.identity);
        b.GetComponent<BulletController>().speed = 10.0f;
    }
}
