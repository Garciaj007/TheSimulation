using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    //Public Members
    [Header("Acceleration")]
    public float acceleration = 2000f;
    public float decceleration = 5f;
    [Header("Max Speed")]
    public float maxWalkSpeed = 10f;
    public float maxRunSpeed = 15f;
    [Space]
    public float airResistence = 0.2f;
    public float jumpForce = 300f;
    public float maxSlope = 60f;
    public ForceMode forceMode;

    //Private Members
    private Rigidbody rigid;
    private Vector2 movement;
    private bool isGrounded;
    private float walkDeccelVelx, walkDeccelVelz;

	void Start () {
        rigid = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        float maxSpeed = 0;

        //If Running change max speed
        if (Input.GetKey(KeyCode.LeftShift))
            maxSpeed = maxRunSpeed;
        else
            maxSpeed = maxWalkSpeed;

        //Gets the players movement speed
        movement = new Vector2(rigid.velocity.x, rigid.velocity.z);

        //limits the players walk speed
        if (movement.magnitude > maxSpeed)
        {
            movement = movement.normalized * maxSpeed;
        }
    }

    private void FixedUpdate()
    {
        //Moves the player with the movement speed
        rigid.velocity = new Vector3(movement.x, rigid.velocity.y, movement.y);

        //Deccelerates the player
        if (isGrounded)
        {
            rigid.velocity = new Vector3(Mathf.SmoothDamp(rigid.velocity.x, 0, ref walkDeccelVelx, decceleration), rigid.velocity.y, Mathf.SmoothDamp(rigid.velocity.z, 0, ref walkDeccelVelz, decceleration));
        }

        if (isGrounded)
            //Accelerates the Player
            rigid.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration, forceMode);
        else
            //Restricts movement while in the air
            rigid.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration * airResistence, 0, Input.GetAxis("Vertical") * acceleration * airResistence, forceMode);

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
            rigid.AddForce(Vector3.up * jumpForce, forceMode);
    }


    private void OnCollisionStay(Collision collision)
    {
        //Checks whether the contact point is less then the max slope to be considered as grounded
        foreach(ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
                isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //when player is not on ground
        isGrounded = false;
    }
}
