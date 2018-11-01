using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{
    //Public Members
    public Transform weapon;
    public float walkSpeed, runSpeed, xLookSensitivity, yLookSensitivity, jumpForce, maxDistance;
    public ForceMode forceMode = ForceMode.Force;
    public LayerMask mask;

    //Private Members
    Camera playerCam;
    Animator anim;
    Rigidbody rigid;
    Vector3 motion, rotation, cameraRotation;
    float speed;
    bool jump, isGrounded;

    private void Start()
    {
        //Get Components
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerCam = GetComponentInChildren<Camera>();

        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();

        //Calculate players movement
        Vector3 horizontalMotion = transform.right * Input.GetAxis("Horizontal");
        Vector3 verticalMotion = transform.forward * Input.GetAxis("Vertical");
        motion = (horizontalMotion + verticalMotion).normalized * speed;

        //Calculate vertical & horizontal rotation * the look Sensitivity;
        rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0) * xLookSensitivity;
        cameraRotation = new Vector3(Input.GetAxis("Mouse Y"), 0, 0) * yLookSensitivity;
    }

    private void FixedUpdate()
    {
        //For jumping
        if (jump && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce, forceMode);
        }

        //Moving using WASD
        rigid.AddForce(motion, forceMode);

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

        anim.SetFloat("SpeedX", rigid.velocity.x);
        anim.SetFloat("SpeedY", rigid.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Entered: " + collision.collider.name);
        //If the object touches the ground reset jump
        if (collision.collider.tag == "Ground")
        {
            jump = false;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // if the player leaves the ground prevent additional jumping
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void CheckInput()
    {
        //Check if Fire1 was pressed
        if (Input.GetAxis("Fire1") == 1)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxDistance, mask))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }

        //Check if jump was pressed
        if (Input.GetAxis("Jump") == 1 && !jump)
            jump = true;

        //Check if the left shift is active or not
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}
