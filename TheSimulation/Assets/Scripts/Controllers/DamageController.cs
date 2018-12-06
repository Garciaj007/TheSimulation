using UnityEngine;

public class DamageController : MonoBehaviour {

    public enum DamageType : int {Passive, Constant}
    public DamageType type;
    public float startingDamage;

    private float damageAmount;

    private EntityController entity;
    private Timer damageCooldown;
    private Collider c;

    private void Awake()
    {
        damageCooldown = gameObject.AddComponent<Timer>();
        damageCooldown.Duration = Time.deltaTime;
        damageCooldown.OneShot = false;
        damageCooldown.TimerDone += Damage;

        damageAmount = startingDamage;

        entity = GetComponent<EntityController>();
        entity.HealthChanged += SetDamage;
    }

    private void SetDamage()
    {
        if(type == DamageType.Passive)
        {
            damageAmount = startingDamage * (entity.Health / entity.EntityProperties.maxHealth);
        }
    }

    private void Damage()
    {
        c.GetComponent<EntityController>().Damage(damageAmount * Time.deltaTime);
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
