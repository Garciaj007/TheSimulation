using UnityEngine;

public class AimController : MonoBehaviour {

    public Vector2 lookSensitivity;
    public float SmoothDamping;

    private float xRotation, yRotation;
    private float currentXRot, currentYRot;
    private float xRotVelocity, yRotVelocity;

    private Camera playerCam;
    private Rigidbody rigid;
    private Vector3 rotation, cameraRotation;

	// Use this for initialization
	void Start () {
        playerCam = GetComponentInChildren<Camera>();
        rigid = GetComponent<Rigidbody>();

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Calculate vertical & horizontal rotation * the look Sensitivity;
        rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0) * lookSensitivity.x;
        cameraRotation = new Vector3(Input.GetAxis("Mouse Y"), 0, 0) * lookSensitivity.y;
    }

    private void FixedUpdate()
    {
        //Rotating the player horizontaly
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rotation));

        //Rotating the camera vertically
        if (playerCam != null)
        {
            playerCam.transform.Rotate(-cameraRotation);
            if (playerCam.transform.localRotation.x > 0.5)
            {
                playerCam.transform.localRotation = new Quaternion(0.55f, 0, 0, playerCam.transform.localRotation.w);
            }
            if (playerCam.transform.localRotation.x < -0.5)
            {
                playerCam.transform.localRotation = new Quaternion(-0.55f, 0, 0, playerCam.transform.localRotation.w);
            }
        }
    }
}
