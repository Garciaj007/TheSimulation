using UnityEngine;
using UnityEngine.UI;

public class EntitiyBarController : MonoBehaviour {

    public EntityController entity;

    private Image healthBar;
    private Color healthBarColor;

	// Use this for initialization
	void Start () {
        if (!entity)
            entity = GetComponentInParent<EntityController>();

        healthBar = GetComponent<Image>();
        entity.HealthChanged += UpdateHealthBar;
        healthBarColor = healthBar.color;
        healthBar.color = new Color(1, 1, 1, 0.05f);
	}
	
	public void UpdateHealthBar()
    {
        float amount = entity.Health / entity.EntityProperties.maxHealth;
        healthBar.fillAmount = amount;
        healthBar.color = Color.Lerp(healthBarColor, new Color(1, 1, 1, 0.05f), amount);
    }
}
