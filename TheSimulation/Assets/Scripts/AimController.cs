using UnityEngine;

public class AimController : MonoBehaviour {

    //Public Members
    //Speed of rotation
    public Vector2 lookSensitivity;
    //Damping the jitter of mouse movement
    [Range(0, 0.2f)]
    public float smoothDamp;

    //Private Members
    private Camera playerCam;
    private Rigidbody rigid;
    private float xRotation, yRotation;
    private float smoothedXRot, smoothedYRot;
    private float xRotVelocity, yRotVelocity;

	void Start () {
        playerCam = GetComponentInChildren<Camera>();
        rigid = GetComponent<Rigidbody>();
        Cursor.visible = false; // Hide Cursor
    }
	
	void Update () {
        //Original Rotation
        xRotation += Input.GetAxis("Mouse Y") * lookSensitivity.y;
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity.x;

        //Maximum X rotation
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        //Smoothed Rotation by Smooth Damp
        smoothedXRot = Mathf.SmoothDamp(smoothedXRot, xRotation, ref xRotVelocity, smoothDamp);
        smoothedYRot = Mathf.SmoothDamp(smoothedYRot, yRotation, ref yRotVelocity, smoothDamp);
    }

    private void FixedUpdate()
    {
        //Rotate camera and player
        rigid.rotation = Quaternion.Euler(0, smoothedYRot, 0);
        playerCam.transform.rotation = Quaternion.Euler(-smoothedXRot, smoothedYRot, 0);
    }
}
