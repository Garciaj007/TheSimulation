using UnityEngine;

//To be worked on
public struct Rules
{
    //Spell Type
    public enum ElementalType : int { Fire, Water, Ice, Wind, Earth, Time, Force }
}

//Spell Properties
[System.Serializable]
public struct SpellProperties
{
    public bool continuos; //is run more than once
    public bool passive; //does not require target
    public float manaCost;
    public float staminaCost;
    public float healIndex; //# of heals
    public float damage;
    public float cooldown; //duration till next cast
}

//Base Spell Class
[System.Serializable]
public abstract class Spell
{
    //Protected Members
    protected SpellProperties properties; //Spell Properties

    //Properties
    public Rules.ElementalType Type { get; private set; } //Element Type
    public EntityController Entity { get; private set; }
    public PlayerController Player { get; private set; } //Player Controller attached to
    public SpellProperties Properties { get { return properties; } }  //Spell Properties 
    public string WarnMsg { get; private set; } //Msg to be display (WARN)
    public string ErrorMsg { get; protected set; } //Msg to be display (ERROR)

    //Constructor
    public Spell(PlayerController player, Rules.ElementalType type)
    {
        Entity = player.GetComponent<EntityController>();
        Player = player;
        Type = type;
        WarnMsg = "NULLMSG";
        ErrorMsg = "NULLMSG";
    }

    //Virtual Methods
    public virtual bool Cast(RaycastHit hit) { return true; }
    public virtual bool Cast() { return true; }

    //Checks if Spell can Run and doesnt require target
    public bool Calculate()
    {
        //If there is allocated Mana
        if (Player.Mana - Properties.manaCost > 0)
        {
            //If there is allocated stamina
            if (Player.Stamina - Properties.staminaCost > 0)
            {
                return true; //Success
            }
            else
            {
                WarnMsg = "Low Energy";
            }
        }
        else
        {
            WarnMsg = "Not Enough Mana";
        }

        return false;//Failed
    }

    //Checks if Spell can Run and requires target
    public bool Calculate(RaycastHit hit)
    {
        //If target can be affected
        if (hit.collider.GetComponent<EntityController>() || hit.collider.GetComponent<PlayerController>())
        {
            //If there is allocated Mana
            if (Player.Mana - Properties.manaCost > 0)
            {
                //If there is allocated stamina
                if (Player.Stamina - Properties.staminaCost > 0)
                {
                    return true; //Success
                }
                else
                {
                    WarnMsg = "Low Energy";
                }
            }
            else
            {
                WarnMsg = "Low Mana";
            }
        }
        else
        {
            WarnMsg = "No Target Found";
        }

        return false; //Failed 
    }

    //Subtract spell costs and etc.
    protected void Use()
    {
        Player.Mana = -Properties.manaCost;
        Player.Stamina = -Properties.staminaCost;
    }

    //If a spell has damage, damage enemy/entity/object/player
    protected void InflictDamage(RaycastHit hit)
    {
        if (hit.collider.GetComponent<EntityController>())
            hit.collider.GetComponent<EntityController>().Damage(Properties.damage);
    }
}

