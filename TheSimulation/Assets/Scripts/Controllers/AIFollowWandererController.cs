using UnityEngine;

public class AIFollowWandererController : MonoBehaviour {

    public Transform wanderer;

	// Update is called once per frame
	void FixedUpdate () {
        //Vector3 followPosition = this.transform.position + wanderer.position;
        transform.rotation = Quaternion.Euler(0,wanderer.rotation.eulerAngles.y, 0);
        transform.position = wanderer.position;
	}
}
