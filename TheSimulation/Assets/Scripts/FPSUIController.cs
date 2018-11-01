using UnityEngine;
using UnityEngine.UI;

public class FPSUIController : MonoBehaviour {

    public Image manaLevelBar;
    public Image manaBar;
    public Image healthBar;
    public Image staminaBar;
    public PlayerController player;

	// Use this for initialization
	void Start () {
        player.ManaLevelChanged += UpdateManaLevelBar;
        player.ManaChanged += UpdateManaBar;
        player.HealthChanged += UpdateHealthBar;
        player.StaminaChanged += UpdateStaminaBar;
	}

    public void UpdateManaLevelBar()
    {
        float amount = player.ManaLevel;
        Color c = manaLevelBar.color;
        manaLevelBar.fillAmount = amount;
        manaLevelBar.color = Color.Lerp(new Color(c.r, c.g, c.b, 0.6f), new Color(c.r, c.g, c.b, 0.05f), amount);
    }

    public void UpdateManaBar()
    {
        float amount = player.Mana / player.PlayerProperties.maxMana;
        Color c = manaBar.color;
        manaBar.fillAmount = amount;
        manaBar.color = Color.Lerp(new Color(c.r, c.g, c.b, 0.6f), new Color(c.r, c.g, c.b, 0.05f), amount);
    }

    public void UpdateHealthBar()
    {
        float amount = player.Health / player.EntityProperties.maxHealth;
        Color c = healthBar.color;
        healthBar.fillAmount = amount;
        healthBar.color = Color.Lerp(new Color(c.r, c.g, c.b, 0.6f), new Color(c.r, c.g, c.b, 0.05f), amount);
    }

    public void UpdateStaminaBar()
    {
        float amount = player.Stamina / player.PlayerProperties.maxStamina;
        Color c = staminaBar.color;
        staminaBar.fillAmount = amount;
        staminaBar.color = Color.Lerp(new Color(c.r, c.g, c.b, 0.6f), new Color(c.r, c.g, c.b, 0.05f), amount);
    }
	

}
