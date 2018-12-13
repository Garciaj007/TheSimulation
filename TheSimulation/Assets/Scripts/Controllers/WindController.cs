using UnityEngine;

public class WindController : MonoBehaviour {

    private WindZone wind;
    private Timer windTimer;
    private bool direction;

    public float speed;

	// Use this for initialization
	void Start () {
        wind = GetComponent<WindZone>();

        windTimer = gameObject.AddComponent<Timer>();
        windTimer.Duration = speed;
        windTimer.TimerDone += ChangeDirection;
        windTimer.Begin();
	}
	
	// Update is called once per frame
	void Update () {
        if (direction)
            wind.windMain = Mathf.Lerp(-1.0f, 1.0f, Time.time);
        else
            wind.windMain = Mathf.Lerp(1.0f, 1.0f, Time.time);
	}

    public void ChangeDirection()
    {
        direction = !direction;
    }

    
}
