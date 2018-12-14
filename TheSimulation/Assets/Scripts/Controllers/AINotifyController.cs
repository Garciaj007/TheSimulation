using UnityEngine;

public class AINotifyController : MonoBehaviour {

    private AIController controller;
    private Transform playerTransform;

	// Use this for initialization
	void Start () {
        controller = GetComponent<AIController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(controller.state == AIController.AIState.Attack)
        {
            transform.LookAt(playerTransform.position, transform.up);

            Debug.Log("Notifying other Enemies");
        }
	}
}
