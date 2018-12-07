using UnityEngine;

public class MovementController : MonoBehaviour {

    //Public Members
    public Animator anim;
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
    private Vector3 movementInDirection;
    private Vector2 movement;
    private bool isGrounded, isRunning, isCrouching, isJumping;
    private float walkDeccelVelx, walkDeccelVelz;
    private float heading;

	void Start () {
        rigid = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        
        float heading = Mathf.Atan2(transform.right.z, transform.right.x) * Mathf.Rad2Deg;
        movementInDirection = Quaternion.Euler(0, heading, 0) * new Vector3(movement.x, 0, movement.y);

        float maxSpeed = 0;

        if (Input.GetKey(KeyCode.LeftControl))
            isCrouching = true;
        else
            isCrouching = false;

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
        if (!isCrouching)
        {
            //Moves the player with the movement speed
            rigid.velocity = new Vector3(movement.x, rigid.velocity.y, movement.y);

            //Deccelerates the player
            if (isGrounded)
            {
                isJumping = false;
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
            {
                isJumping = true;
                rigid.AddForce(Vector3.up * jumpForce, forceMode);
            }
        }
    }

    private void LateUpdate()
    {
        anim.SetFloat("SpeedX", Utilities.Map(movementInDirection.x, 0, maxRunSpeed, 0, 2));
        anim.SetFloat("SpeedY", Utilities.Map(movementInDirection.z, 0, maxRunSpeed, 0, 2));

        anim.SetBool("Crouching", isCrouching);
        anim.SetBool("Jumping", isJumping);
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
