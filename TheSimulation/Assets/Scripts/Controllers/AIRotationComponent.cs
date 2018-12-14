using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotationComponent : MonoBehaviour {

    [Range(-10.0f, 10.0f)]
    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, speed * Time.deltaTime, transform.rotation.eulerAngles.z);
	}
}
