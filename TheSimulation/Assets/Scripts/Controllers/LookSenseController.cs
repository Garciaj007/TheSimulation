using UnityEngine;
using UnityEngine.AI;

public class LookSenseController : SenseController {

    public int FOV = 45;
    public int viewDistance = 100;

    [SerializeField]
    private Transform playerTransform;
    private Vector3 rayDirection;

    private void OnDrawGizmos()
    {
        if (playerTransform == null)
            return;

        Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        Vector3 frontRay = transform.position + (transform.forward * viewDistance);

        Vector3 leftRay = frontRay;
        leftRay.x += FOV * 0.5f;

        Vector3 rightRay = frontRay;
        rightRay.x -= FOV * 0.5f;

        Vector3 upRay = frontRay;
        upRay.y += FOV * 0.5f;

        Vector3 downRay = frontRay;
        downRay.y -= FOV * 0.5f;

        Debug.DrawLine(transform.position, frontRay, Color.green);
        Debug.DrawLine(transform.position, leftRay, Color.green);
        Debug.DrawLine(transform.position, rightRay, Color.green);
        Debug.DrawLine(transform.position, upRay, Color.green);
        Debug.DrawLine(transform.position, downRay, Color.green);
    }

    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= detectionRate)
        {
            DetectEntity();
        }
    }

    void DetectEntity()
    {
        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;

        if(Vector3.Angle(rayDirection, transform.forward) < FOV)
        {
            if(Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Identifier id = hit.collider.GetComponent<Identifier>();
                if(id != null)
                {
                    if (controller != null)
                    {
                        if (id.identity != identity)
                        {
                            controller.playerVisable = true;
                            return;
                        }
                    }
                }
            }
        }

        if(controller != null)
        controller.playerVisable = false;
    }
}
