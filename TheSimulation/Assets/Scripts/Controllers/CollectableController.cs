using UnityEngine;

public class CollectableController : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ShootController shoot = other.GetComponent<ShootController>();
            SpellLibrary.GetSpell(ref shoot.spells, shoot);
        }
    }
}
