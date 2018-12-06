using UnityEngine;

public class GravityController : MonoBehaviour {

    private Rigidbody rigid;
    private bool gravityUp, gravityDown, gravityForward, gravityBack, gravityLeft, gravityRight;
    private bool[] gravity;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        gravity = new bool[6];
	}
	
	// Update is called once per frame
	void Update () {

        //up
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            for (int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = false;
            }

            gravity[1] = true;
        }
        //down
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            for(int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = false;
            }

            gravity[4] = true;
        }
        //forward
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            for (int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = false;
            }

            gravity[0] = true;
        }
        //back
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            for (int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = false;
            }

            gravity[3] = true;
        }
        //left
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            for (int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = false;
            }

            gravity[2] = true;
        }
        //right
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            for (int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = false;
            }

            gravity[5] = true;
        }

    }

    private void FixedUpdate()
    {
        //up
        if (gravity[1])
        {
            rigid.useGravity = false;
            rigid.velocity = new Vector3(rigid.velocity.x, 9.8f, rigid.velocity.z);
        }
        //down
        if (gravity[4])
        {
            rigid.useGravity = true;
        }
        //forward
        if (gravity[0])
        {
            rigid.useGravity = false;
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, 9.8f);
        }
        //back
        if (gravity[3])
        {
            rigid.useGravity = false;
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, -9.8f);
        }
        //left
        if (gravity[2])
        {
            rigid.useGravity = false;
            rigid.velocity = new Vector3(9.8f, rigid.velocity.y, rigid.velocity.z);
        }
        //right
        if (gravity[5])
        {
            rigid.useGravity = false;
            rigid.velocity = new Vector3(-9.8f, rigid.velocity.y, rigid.velocity.z);
        }
    }
}
