using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class ShootController : MonoBehaviour
{
    //Delegates
    public delegate void SpellSwitchEventHandler();
    public delegate void WarnEventHandler();
    public delegate void ErrorEventHandler();

    //Events
    public event SpellSwitchEventHandler SpellSwitched;
    public event WarnEventHandler SpellWarned;
    public event ErrorEventHandler SpellCrashed;

    //Public Members
    public float maxDistance;
    public LayerMask mask;

    //Private members
    public List<Spell> spells = new List<Spell>();
    private Camera playerCam;
    private Timer cooldown;
    private int index = 0;
    private bool shot = false;

    //Properties
    public Spell CurrentSpell { get; private set; }

    void Start()
    {
        //Get Componenets
        playerCam = GetComponentInChildren<Camera>();

        //Add a timer
        cooldown = gameObject.AddComponent<Timer>();
        cooldown.TimerDone += Shoot;

        //Add Spells
        SpellLibrary.GetSpell(ref spells, this);
        
        //AddSpell(new Force_NeutraliseGravity(player));
        //AddSpell(new Force_FlipGravity(player));
        //AddSpell(new Ice_Freeze(player));
        //AddSpell(new Fire_Burn(player));

        //Get Current Spell
        CurrentSpell = spells[0];
        OnSpellSwitched();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown.Pause();
        cooldown.Duration = CurrentSpell.Properties.cooldown;

        //if the spell is repetitve
        if (CurrentSpell.Properties.continuos)
        {
            //Check if Fire1 was pressed
            if (Input.GetAxis("Fire1") == 1)
            {
                cooldown.Begin();
            }
            else
            {
                cooldown.Pause();
            }
        }
        else
        {
            //spell not repetative
            if(Input.GetAxis("Fire1") == 1)
            {
                //check if shot was called
                if (!shot)
                {
                    //shoot
                    shot = true;
                    Shoot();
                }
            }
            else
            {
                shot = false;
            }
        }

        Select();
    }

    private void Shoot()
    {
        //if spell does require hitscan
        if (!spells[index].Properties.passive)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxDistance, mask))
            {
                //Check Spell
                if (spells[index].Calculate(hit))
                {
                    //Cast Spell
                    if (spells[index].Cast(hit))
                    {
                        //TO DO
                    }
                    else
                    {
                        OnSpellCrashed(); //spell did not succed
                    }
                }
                else
                {
                    OnSpellWarned(); //spell failed checks
                }
            }
        }
        else //spell does not require hitscan
        {
            //Check
            if (spells[index].Calculate())
            {
                //Cast
                if (spells[index].Cast())
                {
                    //TO DO
                }
                else
                {
                    OnSpellCrashed(); //Failed
                }
            }
            else
            {
                OnSpellWarned(); //Failed Checks 
            }
        }
    }

    //Selects Spell with ScrollWheel
    private void Select()
    {
        float d = Input.GetAxis("Mouse ScrollWheel");

        //Checks direction of scroll
        if (d > 0f)
        {
            index++;
        }
        else if (d < 0f)
        {
            index--;
        }

        //limits index to the spell Array
        if (index < 0)
            index = spells.Count - 1;
        if (index > spells.Count - 1)
            index = 0;

        //Set Current Spell
        CurrentSpell = spells[index];
        OnSpellSwitched();
    }

    public void AddSpell(Spell s)
    {
        s.Player = GetComponent<PlayerController>();
        s.Entity = GetComponent<EntityController>();
        spells.Add(s);
    }

    //When Spell has been Switched
    private void OnSpellSwitched()
    {
        if (SpellSwitched != null)
            SpellSwitched();
    }

    //When Spell Fails Checks
    private void OnSpellWarned()
    {
        if (SpellWarned != null)
            SpellWarned();
    }

    //When Spell Crashed
    private void OnSpellCrashed()
    {
        if (SpellCrashed != null)
            SpellCrashed();
    }
}
