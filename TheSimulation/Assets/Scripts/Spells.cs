using UnityEngine;
public class Force_MoveObject : Spell {

    public Force_MoveObject(PlayerController p) : base(p, Rules.ElementalType.Force)
    {
        properties.manaCost = 10f;
        properties.staminaCost = 10f;
    }

    public override bool Cast(RaycastHit hit)
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.AddForce(Vector3.forward * 100f);
            return true;
        }

        return false;
    }
}

public class Force_NeutraliseGravity : Spell
{
    public Force_NeutraliseGravity(PlayerController p) : base (p, Rules.ElementalType.Force)
    {
        properties.manaCost = 12f;
        properties.staminaCost = 6f;
    }

    public override bool Cast(RaycastHit hit)
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.useGravity = false;
            return true;
        }

        return false;
    }
}

public class Force_FlipGravity : Spell
{
    public Force_FlipGravity(PlayerController p):base(p, Rules.ElementalType.Force)
    {
        properties.manaCost = 20f;
        properties.staminaCost = 19f;
    }

    public override bool Cast(RaycastHit hit)
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.useGravity = false;
            hit.rigidbody.velocity = new Vector3(0, 9.8f, 0);
            return true;
        }

        return false;
    }
}

public class Ice_Freeze : Spell
{
    public Ice_Freeze(PlayerController p) : base (p, Rules.ElementalType.Ice)
    {
        properties.manaCost = 10f;
        properties.staminaCost = 10f;
    }

    public override bool Cast(RaycastHit hit)
    {
        throw new System.NotImplementedException();
    }
}
