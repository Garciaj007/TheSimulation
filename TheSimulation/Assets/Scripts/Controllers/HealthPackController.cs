using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour {

    public GameObject healthPack;

    [SerializeField]
    private float healthRegenerated = 10.0f;
    [SerializeField]
    private float cooldown = 2.0f;

    private Timer cooldownTimer;
    private bool consumed;

    private void Awake()
    {
        cooldownTimer = gameObject.AddComponent<Timer>();
        cooldownTimer.Duration = cooldown;
        cooldownTimer.TimerDone += MakeAvailable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!consumed)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            {
                EntityController entity = other.gameObject.GetComponent<EntityController>();
                if (entity.Health < entity.EntityProperties.maxHealth)
                {
                    entity.Heal(healthRegenerated);
                    consumed = true;
                    cooldownTimer.Begin();
                    healthPack.SetActive(false);
                }
            }
        }
    }

    private void MakeAvailable()
    {
        consumed = false;
        cooldownTimer.Pause();
        healthPack.SetActive(true);
    }
}
