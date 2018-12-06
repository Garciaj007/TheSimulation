using UnityEngine;

public class LookSenseController : SenseController {

    public int FOV = 45;
    public int viewDistance = 100;

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

        Debug.DrawLine(transform.position, frontRay, Color.green);
        Debug.DrawLine(transform.position, leftRay, Color.green);
        Debug.DrawLine(transform.position, rightRay, Color.green);
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
                Identifier ident = hit.collider.GetComponent<Identifier>();
                if(ident != null)
                {
                    if(ident.identity != identity)
                    {
                        Debug.Log("Enemy Detected");
                    }
                }
            }
        }
    }
}
