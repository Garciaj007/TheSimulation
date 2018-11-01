using UnityEngine;

public struct Rules
{
    public enum ElementalType : int {Fire, Water, Ice, Wind, Earth, Time, Force}
}

[System.Serializable]
public struct SpellProperties
{
    public bool passive;
    public float manaCost;
    public float staminaCost;
    public float healIndex;
    public float damage;
}

[System.Serializable]
public abstract class Spell
{
    protected SpellProperties properties;

    public Rules.ElementalType Type { get; private set; }
    public PlayerController Player { get; private set; }
    public SpellProperties Properties { get { return properties; } }

    public Spell(PlayerController player, Rules.ElementalType type)
    {
        Player = player;
        Type = type;
    }

    public virtual bool Cast(RaycastHit hit) { return true; }
    public virtual bool Cast() { return true; }

    public bool Calculate()
    {
        if (Player.Mana - Properties.manaCost > 0)
        {
            if (Player.Stamina - Properties.staminaCost > 0)
            {
                return true;
            }
        }

        return false;
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

    protected void InflictDamage(RaycastHit hit)
    {
        if (hit.collider.GetComponent<EntityController>())
            hit.collider.GetComponent<EntityController>().Damage(Properties.damage);

        if (hit.collider.GetComponent<PlayerController>())
            hit.collider.GetComponent<PlayerController>().Damage(Properties.damage);
    }
}

