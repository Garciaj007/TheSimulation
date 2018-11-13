using UnityEngine;

public class DamageController : MonoBehaviour {

    public float damageAmount;

    private PlayerController player;
    private Timer damageCooldown;
    private Collider c;

    private void Start()
    {
        damageCooldown = gameObject.AddComponent<Timer>();
        damageCooldown.Duration = Time.deltaTime;
        damageCooldown.OneShot = false;
        damageCooldown.TimerDone += Damage;
    }

    private void Damage()
    {
        c.GetComponent<PlayerController>().Damage(damageAmount * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        damageCooldown.Begin();
        c = other;
    }

    private void OnTriggerExit(Collider other)
    {
        c = other;
        damageCooldown.Pause();
        damageCooldown.Restart();
    }
}
