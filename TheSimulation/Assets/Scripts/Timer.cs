using UnityEngine;

public class Timer : MonoBehaviour {

    public delegate void TimerEventHandler();
    public event TimerEventHandler TimerDone;

    private float elapsed = 0;
    private bool active = false;

    public float Duration { private get; set; }
    public bool OneShot { get; set; }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if(elapsed > Duration)
            {
                OnTimerDone();

                if (OneShot)
                    Pause();
                else
                    Restart();
            }

            elapsed += Time.deltaTime;
        }
	}

    public void Begin()
    {
        active = true;
    }

    public void Pause()
    {
        active = false;
    } 

    public void UnPause()
    {
        active = true;
    }

    public void Restart()
    {
        elapsed = 0;
    }

    public void OnTimerDone()
    {
        if (TimerDone != null)
            TimerDone();
    }
}
