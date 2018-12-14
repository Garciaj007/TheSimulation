using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private Rigidbody rigid;
    private bool isProjectile = false;

    public float tolerance = 1.0f;
    public float damageMultiplier = 0.1f;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigid.velocity.magnitude > tolerance)
        {
            isProjectile = true;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (isProjectile)
        {
            EntityController c = collision.gameObject.GetComponent<EntityController>();

            if (c)
                c.Damage((rigid.velocity.magnitude * rigid.mass) * damageMultiplier);
        }
    }

}
