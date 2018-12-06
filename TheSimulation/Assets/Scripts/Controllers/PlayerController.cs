using UnityEngine;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(AimController))]
[RequireComponent(typeof(ShootController))]
public class PlayerController : MonoBehaviour {

    //Delegates
    public delegate void ManaLevelEventListener();
    public delegate void ManaEventListener();
    public delegate void StaminaEventListener();

    //Events
    public event ManaLevelEventListener ManaLevelChanged;
    public event ManaEventListener ManaChanged;
    public event StaminaEventListener StaminaChanged;

    //Private Members
    [SerializeField]
    private PlayerProperties playerProperties;
    private float manaLevel, mana, stamina;

    //Properties
    public bool UsingStamina { get; set; }
    public PlayerProperties PlayerProperties { get { return playerProperties; } set { playerProperties = value; } }

    /*Note Checks if 0 < var < max */
    public float ManaLevel
    {
        get { return manaLevel; }
        private set { manaLevel = value; OnManaLevelChanged(); }
    }

    public float Mana
    {
        get { return mana; }
        set { if (mana + value > PlayerProperties.maxMana) mana = PlayerProperties.maxMana; else if (mana + value < 0) mana = 0; else mana += value; OnManaChanged(); }
    }

    public float Stamina
    {
        get { return stamina; }
        set { if (stamina + value > PlayerProperties.maxStamina) stamina = PlayerProperties.maxStamina; else if (stamina + value < 0) stamina = 0; else stamina += value; OnStaminaChanged(); }
    }

    protected void Start () {
        Reset();
	}
	
	protected void Update () {
        //Health = 0; //debugging

        //Update Mana Level from field
        ManaLevel = ManaField.Instance.GetManaSample(transform.position);
        //Update Mana
        Mana = ManaLevel * 0.01f * PlayerProperties.manaRecoveryRate;

        //if not using stamina increase by rate specified by player
        if(!UsingStamina)
            Stamina = 0.01f * PlayerProperties.staminaRecoveryRate;

        ////if player regenerates, increase by rate
        //if (EntityProperties.regen)
        //    Health = 0.01f * EntityProperties.healthRecoverRate;

        ////NOTE We Dont want to do this
        //if (Health < 0)
        //{
        //    //Do Something...
        //}
    }

    //Resets Properties
    protected void Reset()
    {
        //Health = EntityProperties.maxHealth + 1;
        Mana = PlayerProperties.maxMana;
        Stamina = PlayerProperties.maxStamina;
    }

    //Recover Stamina by amount
    protected void RecoverStamina(float amount)
    {
        Stamina = amount;
    }

    //When Manalevel changes
    protected void OnManaLevelChanged()
    {
        if (ManaLevelChanged != null)
            ManaLevelChanged();
    }

    //When mana changes
    protected void OnManaChanged()
    {
        if (ManaChanged != null)
            ManaChanged();
    }

    //When Stamina Changes
    protected void OnStaminaChanged()
    {
        if (StaminaChanged != null)
            StaminaChanged();
    }
}

//Player Properties
[System.Serializable]
public class PlayerProperties
{
    [Header("Max")]
    public float maxStamina; //Max Stamina Capacity 
    public float maxMana; //Max Mana Capcity
    [Header("Rates")]
    public float staminaRecoveryRate;//Rate of stamina recovery
    public float manaRecoveryRate;//rate of mana recovery
    public float staminaUsageRate;//usage rate of stamina i.e. Running
}
