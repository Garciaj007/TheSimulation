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
        properties.manaCost = 10f;
        properties.staminaCost = 10f;
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
