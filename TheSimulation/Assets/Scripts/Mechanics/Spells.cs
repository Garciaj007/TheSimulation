﻿using UnityEngine;

//Moves Object Forward
public class Force_MoveObject : Spell {

    public Force_MoveObject() : base(Rules.ElementalType.Force)
    {
        properties.manaCost = 1f;
        properties.staminaCost = 1f;
        properties.cooldown = 0.2f;
        properties.continuos = true;
    }

    public override bool Cast(RaycastHit hit)
    {
        if (hit.rigidbody)
        {
            Use();
            Vector3 direction = hit.transform.position - Player.transform.position;
            hit.rigidbody.AddForce(direction * 100f);
            return true;
        } else
        {
            ErrorMsg = "CAST Failed : No Force Contained";
        }

        return false;
    }
}

//Sets Gravity to false
public class Force_NeutraliseGravity : Spell
{
    public Force_NeutraliseGravity() : base (Rules.ElementalType.Force)
    {
        properties.manaCost = 12f;
        properties.staminaCost = 6f;
    }

    public override bool Cast(RaycastHit hit)
    {
        if (hit.rigidbody)
        {
            Use();
            hit.rigidbody.useGravity = false;
            return true;
        } else
        {
            ErrorMsg = "CAST Failed : No Force Contained";
        }

        return false;
    }
}

//Flips Gravity
public class Force_FlipGravity : Spell
{
    public Force_FlipGravity():base(Rules.ElementalType.Force)
    {
        properties.manaCost = 10f;
        properties.staminaCost = 10f;
        properties.cooldown = Time.deltaTime;
        properties.continuos = false;
        properties.passive = false;
    }

    public override bool Cast(RaycastHit hit)
    {
        if (hit.rigidbody)
        {
            Use();
            hit.rigidbody.useGravity = false;
            hit.rigidbody.velocity = new Vector3(0f, 9.8f, 0);
            return true;
        } else
        {
            ErrorMsg = "CAST Failed : No Force Contained";
        }

        return false;
    }
}

//Freezes enemies, quenches fire
public class Ice_Freeze : Spell
{
    public Ice_Freeze() : base (Rules.ElementalType.Ice)
    {
        properties.manaCost = 0.5f;
        properties.staminaCost = 0.2f;
        properties.cooldown = Time.deltaTime;
        properties.continuos = true;
        properties.passive = false;
    }

    public override bool Cast(RaycastHit hit)
    {
        EntityController e = hit.collider.GetComponent<EntityController>();
        if(e.Type == Rules.ElementalType.Fire)
        {
            Use();
            e.Damage(1f);
            return true;
        }
        if(e.Type == Rules.ElementalType.Ice)
        {
            Use();
            e.Heal(1f);
            return true;
        }
        ErrorMsg = "CAST Failed : Improper Cast Type";
        return false;
    }
}

//Burns Targets
public class Fire_Burn : Spell
{
    public Fire_Burn() : base(Rules.ElementalType.Fire)
    {
        properties.manaCost = 0.5f;
        properties.staminaCost = 0.2f;
        properties.cooldown = Time.deltaTime;
        properties.continuos = true;
        properties.passive = false;
    }

    public override bool Cast(RaycastHit hit)
    {
        EntityController e = hit.collider.GetComponent<EntityController>();
        if (e.Type == Rules.ElementalType.Ice)
        {
            Use();
            e.Damage(1f);
            return true;
        }
        if (e.Type == Rules.ElementalType.Fire)
        {
            Use();
            e.Heal(1f);
            return true;
        }
        ErrorMsg = "CAST Failed : Improper Cast Type";
        return false;
    }
}

