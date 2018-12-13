using UnityEngine;
using System.Collections.Generic;

//To be worked on
public struct Rules
{
    //Spell Type
    public enum ElementalType : int { Fire, Water, Ice, Wind, Earth, Time, Force }
    //This is more for Alchemy, and law of compatibility


    //Input Type
    public enum SystemInput : int { Passive, Forward, Back, Up, Down, Right, Left, View, Auto, Select, Spacial, Dimentional }
    //There are many Type of input that can be created

    // 1 - None(Passive), No input is needed, because spell is applied locally, to player itself or neighboring elements of player.

    // 1 A - Forward, cast a spell in the direction player is facing
    // 1 B - Back, cast a spell opposite to the direction player is facing
    // 1 C - Up, cast a spell above 
    // 1 D - Down, cast a spell downwards
    // 1 E - Left, cast a spell left
    // 1 F - Right, cast a spell Right
    // 1 G - View, cast a spell in the direction of view
    // 1 H - Auto, calculate a spell depending on enviromental factors (mana cost intensive)

    // 2 - Most Common and simplest is the Ray-Collider Input, where a spells only input is a ray and a target.
    //Example: flipping the gravity of an object, setting an object on fire, using a specific object that only needs one point or etc.

    // 3 - Complex Input
    //1-D Points in Space, this spawns a spell/uses a location in space to activate a spell.
    //Using a ray find the value t where 0 < t < max and use that location (Vector3 Component)

    // 4 - N Points in Space, a number of points in space are needed for a spell to function properly
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

public class SpellLibrary
{
    private static List<Spell> masterSpells = new List<Spell>();

    public static void AddSpell(Spell s)
    {
        masterSpells.Add(s);
    }

    public static void GetSpell(ref List<Spell> playerSpells, ShootController shoot)
    {
        if (masterSpells.Count > 1)
        {
            for (int i = 0; i < 5; i++)
            {
                int selection = Random.Range(0, masterSpells.Count);

                if (!playerSpells.Exists(x => x == masterSpells[selection])) {
                    shoot.AddSpell(masterSpells[selection]);
                    return;
                }
            }
        }
    }
}

//Base Spell Class
[System.Serializable]
public abstract class Spell
{
    //Protected Members
    protected SpellProperties properties; //Spell Properties

    //Properties
    public Rules.ElementalType Type { get; private set; } //Element Type
    public EntityController Entity { get; set; }
    public PlayerController Player { get; set; } //Player Controller attached to
    public SpellProperties Properties { get { return properties; } }  //Spell Properties 
    public string Description { get; private set; }
    public string WarnMsg { get; private set; } //Msg to be display (WARN)
    public string ErrorMsg { get; protected set; } //Msg to be display (ERROR)

    //Constructor
    public Spell(Rules.ElementalType type)
    {
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

