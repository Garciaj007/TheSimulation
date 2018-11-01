using UnityEngine;

public struct Rules
{
    public enum ElementalType : int {Fire, Water, Ice, Wind, Earth, Time, Force}
}

[System.Serializable]
public struct SpellProperties
{
    public float manaCost;
    public float staminaCost;
    public float damage;
}

public abstract class Spell
{
    protected SpellProperties properties;

    public Rules.ElementalType Type { get; private set; }
    public PlayerController Player { get; private set; }
    public SpellProperties Properties { get { return properties; } }

    public abstract bool Cast(RaycastHit hit);

    public Spell(PlayerController player, Rules.ElementalType type)
    {
        Player = player;
        Type = type;
    }

    public bool Calculate(RaycastHit hit)
    {
        if (hit.collider.GetComponent<EntityController>() || hit.collider.GetComponent<PlayerController>())
        {
            if (Player.Mana - Properties.manaCost > 0)
            {
                if (Player.Stamina - Properties.staminaCost > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected void Use()
    {
        Player.Mana = -Properties.manaCost;
        Player.Stamina = -Properties.staminaCost;
    }

    protected void Damage(RaycastHit hit)
    {
        if (hit.collider.GetComponent<EntityController>())
            hit.collider.GetComponent<EntityController>().Damage(Properties.damage);

        if (hit.collider.GetComponent<PlayerController>())
            hit.collider.GetComponent<PlayerController>().Damage(Properties.damage);
    }
}

