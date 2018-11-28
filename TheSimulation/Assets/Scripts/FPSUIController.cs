using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSUIController : MonoBehaviour {
    //Public Members

    //Images
    public Image manaLevelBar;
    public Image manaBar;
    public Image healthBar;
    public Image staminaBar;

    //TextMeshPros
    public TextMeshProUGUI spellName;
    public TextMeshProUGUI manaCost;
    public TextMeshProUGUI staminaCost;
    public TextMeshProUGUI healsIndex;
    public TextMeshProUGUI damageIndex;
    public TextMeshProUGUI warnMsg;
    public TextMeshProUGUI errorMsg;

    //Animators
    public Animator warnAnim;
    public Animator errorAnim;

    //Controllers
    public PlayerController player;
    public EntityController entity;
    public ShootController shooter;

    //Colors 
    private Color manaLevelBarColor;
    private Color manaBarColor;
    private Color healthBarColor;
    private Color staminaBarColor;

    //Private Members

    //Timers
    private Timer warnTimer;
    private Timer errorTimer;

	void Start () {
        //Attach Signature
        player.ManaLevelChanged += UpdateManaLevelBar;
        player.ManaChanged += UpdateManaBar;
        entity.HealthChanged += UpdateHealthBar;
        player.StaminaChanged += UpdateStaminaBar;

        shooter.SpellSwitched += UpdateSpellPanel;
        shooter.SpellWarned += DisplayWarning;
        shooter.SpellCrashed += DisplayError;

        //Set colors to original color
        manaLevelBarColor = manaLevelBar.color;
        manaBarColor = manaBar.color;
        healthBarColor = healthBar.color;
        staminaBarColor = staminaBar.color;

        //Timer stuff
        warnTimer = gameObject.AddComponent<Timer>();
        errorTimer = gameObject.AddComponent<Timer>();
        warnTimer.Duration = 4.0f;
        errorTimer.Duration = 4.0f;
        warnTimer.OneShot = true;
        errorTimer.OneShot = true;
        warnTimer.TimerDone += HideWarning;
        errorTimer.TimerDone += HideError;
	}

    //Updates Mana Level Bar 
    public void UpdateManaLevelBar()
    {
        float amount = player.ManaLevel;
        manaLevelBar.fillAmount = amount;
        manaLevelBar.color = Color.Lerp(manaLevelBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    //Updates Mana Bar 
    public void UpdateManaBar()
    {
        float amount = player.Mana / player.PlayerProperties.maxMana;
        manaBar.fillAmount = amount;
        manaBar.color = Color.Lerp(manaBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    //Updates Health Bar 
    public void UpdateHealthBar()
    {
        float amount = entity.Health / entity.EntityProperties.maxHealth;
        healthBar.fillAmount = amount;
        healthBar.color = Color.Lerp(healthBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    //Updates Stamina Bar 
    public void UpdateStaminaBar()
    {
        float amount = player.Stamina / player.PlayerProperties.maxStamina;
        staminaBar.fillAmount = amount;
        staminaBar.color = Color.Lerp(staminaBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    //Updates Spell Selection Panel 
    public void UpdateSpellPanel()
    {
        //Sets text to Spell Properties
        spellName.SetText(shooter.CurrentSpell.ToString());
        manaCost.SetText("" + shooter.CurrentSpell.Properties.manaCost);
        staminaCost.SetText("" + shooter.CurrentSpell.Properties.staminaCost);
        healsIndex.SetText("" + shooter.CurrentSpell.Properties.healIndex);
        damageIndex.SetText("" + shooter.CurrentSpell.Properties.damage);
    }

    //Displays Warning
    public void DisplayWarning()
    {
        warnTimer.Restart();
        warnMsg.text = shooter.CurrentSpell.WarnMsg;

        foreach(ScrollingTextController s in GetComponents<ScrollingTextController>())
        {
            if(s.ID == "warn")
            {
                s.UpdateText();
            }
        }

        warnAnim.SetBool("Display", true);
        warnTimer.Begin();
    }

    //Hide Warning
    public void HideWarning()
    {
        warnAnim.SetBool("Display", false);
    }

    //Displays Error 
    public void DisplayError()
    {
        errorTimer.Restart();
        errorMsg.text = shooter.CurrentSpell.ErrorMsg;

        foreach (ScrollingTextController s in GetComponents<ScrollingTextController>())
        {
            if (s.ID == "error")
            {
                s.UpdateText();
            }
        }

        errorAnim.SetBool("Display", true);
        errorTimer.Begin();
    }

    //Hides Error
    public void HideError()
    {
        errorAnim.SetBool("Display", false);
    }

}
