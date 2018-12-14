using UnityEngine;

public class BulletController : MonoBehaviour {

    private Transform playerTransform;
    private Rigidbody rigid;
    private Timer deathTimer;

    public float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = 2.0f;
        deathTimer.TimerDone += DestroyBullet;
    }

    // Use this for initialization
    void Start()
    {
        rigid.velocity = Vector3.Normalize(playerTransform.position - transform.position) * speed;
        rigid.AddForce(Random.insideUnitSphere, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
