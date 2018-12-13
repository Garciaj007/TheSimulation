using UnityEngine;

public class Timer : MonoBehaviour
{
    //Delegates & Events
    public delegate void TimerEventHandler();
    public event TimerEventHandler TimerDone;

    //Private Members
    private float elapsed = 0;
    private bool active = false;

    //Properties
    public float Duration { private get; set; }
    public bool OneShot { get; set; }

    void Update()
    {
        //if running
        if (active)
        {
            //if the elapsed time is greater than duration
            if (elapsed > Duration)
            {
                OnTimerDone();

                //if only once
                if (OneShot)
                    Pause();
                else
                    Restart();
            }
            //update time by deltaTime
            elapsed += Time.deltaTime;
        }
    }

    //Starts timer
    public void Begin() { active = true; }

    //Pauses timer 
    public void Pause() { active = false; }

    //Resumes Timer
    public void UnPause() { active = true; }

    //Sets time back to 0 
    public void Restart() { elapsed = 0; }

    //When the timer is done
    public void OnTimerDone()
    {
        if (TimerDone != null)
            TimerDone();
    }
}
