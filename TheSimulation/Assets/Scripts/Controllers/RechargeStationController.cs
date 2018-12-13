using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStationController : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EntityController entity = other.gameObject.GetComponent<EntityController>();
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (entity.Health < entity.EntityProperties.maxHealth || player.Stamina < player.PlayerProperties.maxStamina)
            {
                entity.Heal(0.1f);
                player.RecoverStamina(0.1f);
            }
        }
    }

}
