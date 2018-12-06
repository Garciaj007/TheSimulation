using UnityEngine;

public class EntityController : MonoBehaviour {

    //Delegates & Events
    public delegate void HealthEventListener();
    public delegate void DeathEventListener();

    public event HealthEventListener HealthChanged;
    public event DeathEventListener Death;

    //Protected Members
    [SerializeField]
    protected float health, deathDelay;
    [SerializeField]
    protected Rules.ElementalType type;
    [SerializeField]
    protected EntityProperties entityProperties;

    protected Timer destroyTimer;

    //Properties
    public Rules.ElementalType Type { get { return type; } }
    public EntityProperties EntityProperties { get { return entityProperties; } set { entityProperties = value; } }

    public float Health
    {
        get { return health; }
        set { if (health + value > EntityProperties.maxHealth) { health = EntityProperties.maxHealth; } else if (health + value < 0) { health = 0; OnDeath(); } else { health += value; OnHealthChanged(); } }
    }

    protected virtual void Start()
    {
        destroyTimer = gameObject.AddComponent<Timer>();
        destroyTimer.Duration = deathDelay;
        destroyTimer.TimerDone += Destroy;
        destroyTimer.OneShot = true;

        Reset();
    }

	protected virtual void Update () {
        if (EntityProperties.regen)
            Health = 0.01f * EntityProperties.healthRecoverRate;
	}

    //Heals by rate
    public void Heal()
    {
        Health = EntityProperties.healthRecoverRate;
    }

    //Heals by amount
    public void Heal(float amount)
    {
        Health = amount;
    }

    //Damages by amount
    public void Damage(float amount)
    {
        Health = -amount;
    }

    protected virtual void Destroy()
    {
        destroyTimer.TimerDone -= Destroy;
        Destroy(gameObject);
    }


    protected virtual void Reset()
    {
        Health = EntityProperties.maxHealth + 1;
    }

    //When Health is changed
    protected void OnHealthChanged()
    {
        if (HealthChanged != null)
            HealthChanged();
    }

    //When the Entities Health Reaches 0
    protected void OnDeath()
    {
        //Destroys Game Object after amount of time
        destroyTimer.Begin();

        if (Death != null)
        {
            Death();
        }
    }
}

//Entity Properties
[System.Serializable]
public class EntityProperties
{
    [Header("Max")]
    public float maxHealth; //Max Health
    [Header("Rates")]
    public float healthRecoverRate; //Rate of Recovery
    [Header("Abilities")]
    public bool regen; //Regeneration
}


