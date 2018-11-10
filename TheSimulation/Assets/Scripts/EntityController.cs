using UnityEngine;

public class EntityController : MonoBehaviour {

    //Delegates & Events
    public delegate void HealthEventListener();
    public event HealthEventListener HealthChanged;

    //Protected Members
    protected float health;
    [SerializeField]
    protected EntityProperties entityProperties;

    //Properties
    public Rules.ElementalType Type { get; private set; }
    public EntityProperties EntityProperties { get { return entityProperties; } set { entityProperties = value; } }

    public float Health
    {
        get { return health; }
        set { if (health + value > EntityProperties.maxHealth) health = EntityProperties.maxHealth; else if (health + value < 0) health = 0; else health += value; OnHealthChanged(); }
    }

	protected virtual void Update () {
		if(Health < 0)
        {
            //Do Something...
        }

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

    //When Health is changed
    protected void OnHealthChanged()
    {
        if (HealthChanged != null)
            HealthChanged();
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


