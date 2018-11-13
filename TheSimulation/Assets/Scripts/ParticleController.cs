using UnityEngine;

public class ParticleController : MonoBehaviour {

    private ParticleSystem system;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.MainModule main;
    private EntityController entity;

	// Use this for initialization
	void Start () {
        system = GetComponent<ParticleSystem>();
        entity = GetComponentInParent<EntityController>();
        entity.HealthChanged += UpdateParticle;
        main = system.main;
        em = system.emission;
	}

    void UpdateParticle()
    {
        main.startColor = Color.Lerp(new Color(1,1,1,0.2f), main.startColor.color, entity.Health / entity.EntityProperties.maxHealth);
        em.rateOverTime = entity.Health / 2;
    }
}
