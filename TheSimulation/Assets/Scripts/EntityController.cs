using UnityEngine;

public class EntityController : MonoBehaviour {

    public delegate void HealthEventListener();
    public event HealthEventListener HealthChanged;

    protected float health;
    [SerializeField]
    protected EntityProperties entityProperties;

    public Rules.ElementalType Type { get; private set; }
    public EntityProperties EntityProperties { get { return entityProperties; } }

    public float Health
    {
        get { return health; }
        set { if (health + value > EntityProperties.maxHealth) health = EntityProperties.maxHealth; else if (health + value < 0) health = 0; else health += value; OnHealthChanged(); }
    }

	protected virtual void Start () {
        Health = EntityProperties.maxHealth;
	}

	protected virtual void Update () {
		if(Health < 0)
        {
            //Do Something...
        }

        if (EntityProperties.regen)
            Health = 0.01f * EntityProperties.healthRecoverRate;
	}

    public void Heal()
    {
        Health = EntityProperties.healthRecoverRate;
    }

    public void Heal(float amount)
    {
        Health = amount;
    }

    public void Damage(float amount)
    {
        Health = -amount;
    }

    protected void OnHealthChanged()
    {
        if (HealthChanged != null)
            HealthChanged();
    }
}

[System.Serializable]
public class EntityProperties
{
    [Header("Max")]
    public float maxHealth;
    [Header("Rates")]
    public float healthRecoverRate;
    [Header("Abilities")]
    public bool regen;
}


