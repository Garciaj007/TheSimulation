using UnityEngine;

public class PlayerController : EntityController {

    public delegate void ManaLevelEventListener();
    public delegate void ManaEventListener();
    public delegate void StaminaEventListener();

    public event ManaLevelEventListener ManaLevelChanged;
    public event ManaEventListener ManaChanged;
    public event StaminaEventListener StaminaChanged;

    [SerializeField]
    private PlayerProperties playerProperties;
    private float manaLevel, mana, stamina;

    public bool UsingStamina { get; set; }
    public PlayerProperties PlayerProperties { get { return playerProperties; } }

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

    // Use this for initialization
    protected override void Start () {
        //Health = EntityProperties.maxHealth;
        //Mana = PlayerProperties.maxMana;
        //Stamina = PlayerProperties.maxStamina;
	}
	
	// Update is called once per frame
	protected override void Update () {

        ManaLevel = ManaField.Instance.GetManaSample(transform.position);
        Mana = ManaLevel * 0.01f * PlayerProperties.manaRecoveryRate;

        if(!UsingStamina)
            Stamina = 0.01f * PlayerProperties.staminaRecoveryRate;

        if (EntityProperties.regen)
            Health = 0.01f * EntityProperties.healthRecoverRate;

        if (Health < 0)
        {
            //Do Something...
        }

        if (Mana < 0)
        {
            //Do Something...
        }

        if(Stamina < 0)
        {
            //Do Something...
        }
    }

    protected void RecoverStamina(float amount)
    {
        Stamina = amount;
    }

    protected void OnManaLevelChanged()
    {
        if (ManaLevelChanged != null)
            ManaLevelChanged();
    }

    protected void OnManaChanged()
    {
        if (ManaChanged != null)
            ManaChanged();
    }

    protected void OnStaminaChanged()
    {
        if (StaminaChanged != null)
            StaminaChanged();
    }
}

[System.Serializable]
public class PlayerProperties
{
    [Header("Max")]
    public float maxStamina; //Max Stamina Capacity 
    public float maxMana; //Max Mana Capcity
    [Header("Rates")]
    public float staminaRecoveryRate;
    public float manaRecoveryRate;
}
