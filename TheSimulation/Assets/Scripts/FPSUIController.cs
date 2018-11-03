using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSUIController : MonoBehaviour {



    public Image manaLevelBar;
    public Image manaBar;
    public Image healthBar;
    public Image staminaBar;

    public TextMeshProUGUI spellName;
    public TextMeshProUGUI manaCost;
    public TextMeshProUGUI staminaCost;
    public TextMeshProUGUI healsIndex;
    public TextMeshProUGUI damageIndex;
    public TextMeshProUGUI warnMsg;
    public TextMeshProUGUI errorMsg;

    public Animator warnAnim;
    public Animator errorAnim;

    public PlayerController player;
    public ShootController shooter;

    private Color manaLevelBarColor;
    private Color manaBarColor;
    private Color healthBarColor;
    private Color staminaBarColor;

    private Timer warnTimer;
    private Timer errorTimer;

	// Use this for initialization
	void Start () {
        player.ManaLevelChanged += UpdateManaLevelBar;
        player.ManaChanged += UpdateManaBar;
        player.HealthChanged += UpdateHealthBar;
        player.StaminaChanged += UpdateStaminaBar;

        shooter.SpellSwitched += UpdateSpellPanel;
        shooter.SpellWarned += DisplayWarning;
        shooter.SpellCrashed += DisplayError;

        manaLevelBarColor = manaLevelBar.color;
        manaBarColor = manaBar.color;
        healthBarColor = healthBar.color;
        staminaBarColor = staminaBar.color;

        warnTimer = gameObject.AddComponent<Timer>();
        errorTimer = gameObject.AddComponent<Timer>();
        warnTimer.Duration = 4.0f;
        errorTimer.Duration = 4.0f;
        warnTimer.OneShot = true;
        errorTimer.OneShot = true;
        warnTimer.TimerDone += HideWarning;
        errorTimer.TimerDone += HideError;
	}

    public void UpdateManaLevelBar()
    {
        float amount = player.ManaLevel;
        manaLevelBar.fillAmount = amount;
        manaLevelBar.color = Color.Lerp(manaLevelBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    public void UpdateManaBar()
    {
        float amount = player.Mana / player.PlayerProperties.maxMana;
        Color c = manaBar.color;
        manaBar.fillAmount = amount;
        manaBar.color = Color.Lerp(manaBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    public void UpdateHealthBar()
    {
        float amount = player.Health / player.EntityProperties.maxHealth;
        Color c = healthBar.color;
        healthBar.fillAmount = amount;
        healthBar.color = Color.Lerp(healthBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    public void UpdateStaminaBar()
    {
        float amount = player.Stamina / player.PlayerProperties.maxStamina;
        Color c = staminaBar.color;
        staminaBar.fillAmount = amount;
        staminaBar.color = Color.Lerp(staminaBarColor, new Color(1, 1, 1, 0.05f), amount);
    }

    public void UpdateSpellPanel()
    {
        spellName.SetText(shooter.CurrentSpell.ToString());
        manaCost.SetText("" + shooter.CurrentSpell.Properties.manaCost);
        staminaCost.SetText("" + shooter.CurrentSpell.Properties.staminaCost);
        healsIndex.SetText("" + shooter.CurrentSpell.Properties.healIndex);
        damageIndex.SetText("" + shooter.CurrentSpell.Properties.damage);
    }

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

    public void HideWarning()
    {
        warnAnim.SetBool("Display", false);
    }

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

    public void HideError()
    {
        errorAnim.SetBool("Display", false);
    }

}
