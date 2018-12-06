using UnityEngine;

public class ParticleController : MonoBehaviour {

    private ParticleSystem system;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.MainModule main;
    private EntityController entity;

    private float rate;

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        entity = GetComponentInParent<EntityController>();
        entity.HealthChanged += UpdateParticle;
        main = system.main;
        em = system.emission;

        rate = em.rateOverTime.constant;
    }

    void UpdateParticle()
    {
        if (entity)
        {
            main.startColor = Color.Lerp(new Color(1, 1, 1, 0.2f), main.startColor.color, entity.Health / entity.EntityProperties.maxHealth);
            em.rateOverTime = rate * (entity.Health / entity.EntityProperties.maxHealth);
        }
    }
}
