using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotationMatchController : MonoBehaviour {

    public Transform wanderer;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(wanderer.forward, wanderer.up);
	}
}
