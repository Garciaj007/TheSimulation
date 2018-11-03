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
    public string WarnMsg { get; private set; }
    public string ErrorMsg { get; protected set; }

    public Spell(PlayerController player, Rules.ElementalType type)
    {
        Player = player;
        Type = type;
        WarnMsg = "NULLMSG";
        ErrorMsg = "NULLMSG";
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
            } else
            {
                WarnMsg = "Low Energy";
            }
        } else
        {
            WarnMsg = "Not Enough Mana";
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
                } else
                {
                    WarnMsg = "Low Energy";
                }
            } else
            {
                WarnMsg = "Low Mana";
            }
        } else
        {
            WarnMsg = "No Target Found";
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

