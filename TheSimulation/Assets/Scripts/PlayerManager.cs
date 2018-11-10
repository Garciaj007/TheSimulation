using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private PlayerController player;
    private AimController aim;
    private ShootController shooter;
    private MovementController movement;
    private Rigidbody rigid;

    private Vector2 lookSensivity;
    private float acceleration;
    private float decceleration;
    private float jumpForce;
    private float airResistance;
    private PlayerProperties playerProp;
    private EntityProperties entityProp;

    // Use this for initialization
    void Start () {
        player = GetComponent<PlayerController>();
        movement = GetComponent<MovementController>();
        shooter = GetComponent<ShootController>();
        aim = GetComponent<AimController>();
        rigid = GetComponent<Rigidbody>();

        SetManager();
    }

    public void SetManager()
    {
        lookSensivity = aim.lookSensitivity;

        acceleration = movement.acceleration;
        decceleration = movement.decceleration;
        jumpForce = movement.jumpForce;
        airResistance = movement.airResistence;

        playerProp = player.PlayerProperties;
        entityProp = player.EntityProperties;
    }

    //--------------------------------------------------------- AimController Manipulator ------------------------------------------------------

    public void SetAimSensitivity(float x, float y)
    {
        aim.lookSensitivity = new Vector2(x, y);
    }

    public void ResetAimSensitivity()
    {
        aim.lookSensitivity = lookSensivity;
    }

    //------------------------------------------------------- ShooterController Manipulator ---------------------------------------------------

    //TO DO: add a function to jam players cast

    //------------------------------------------------------- MovementController Manipulator ---------------------------------------------------

    public void SetAccel(float x)
    {
        movement.acceleration = x;
    }

    public void ResetAccel()
    {
        movement.acceleration = acceleration;
    }

    public void SetDeccel(float x)
    {
        movement.decceleration = x;
    }

    public void ResetDeccel()
    {
        movement.decceleration = decceleration;
    }

    public void SetJumpForce(float x)
    {
        movement.jumpForce = x;
    }

    public void ResetJumpForce()
    {
        movement.jumpForce = jumpForce;
    }

    public void SetAirResistance(float x)
    {
        movement.airResistence = x;
    }

    public void ResetAirResistance()
    {
        movement.airResistence = airResistance;
    }

    //--------------------------------------------------------- PlayerController Manipulation ------------------------------------------------------

    public void SetPlayerProperties(PlayerProperties prop)
    {
        player.PlayerProperties = prop;
    }

    public void ResetPlayerProperties()
    {
        player.PlayerProperties = playerProp;
    }

    public void SetEntityProperties(EntityProperties prop)
    {
        player.EntityProperties = prop;
    }

    public void ResetEntityProperties()
    {
        player.EntityProperties = entityProp;
    }

}
