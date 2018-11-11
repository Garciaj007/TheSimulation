using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMotionController : MonoBehaviour {

    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rigid.AddForce(Random.insideUnitSphere);
	}
}
