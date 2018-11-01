using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{
    //Public Members
    public float walkSpeed, runSpeed, jumpForce;
    public ForceMode forceMode = ForceMode.Force;

    //Private Members
    Animator anim;
    Rigidbody rigid;
    Vector3 motion;
    float speed;
    bool jump, isGrounded;

    private void Start()
    {
        //Get Components
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMotion();

        //Check if jump was pressed
        if (Input.GetAxis("Jump") == 1 && !jump)
            jump = true;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        else
            speed = walkSpeed;

        if (Mathf.Abs(motion.x) >= walkSpeed - 1 || Mathf.Abs(motion.y) >= walkSpeed - 1)
        {
            //Use Stamina
        }
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

        anim.SetFloat("SpeedX", rigid.velocity.x);
        anim.SetFloat("SpeedY", rigid.velocity.y);
    }

    private void CalculateMotion()
    {
        //Calculate players movement
        Vector3 horizontalMotion = transform.right * Input.GetAxis("Horizontal");
        Vector3 verticalMotion = transform.forward * Input.GetAxis("Vertical");
        motion = (horizontalMotion + verticalMotion).normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
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

    
}
