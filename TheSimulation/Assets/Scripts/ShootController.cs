using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public delegate void SpellSwitchEventHandler();
    public event SpellSwitchEventHandler SpellSwitched;

    public float maxDistance;
    public LayerMask mask;
    public Spell[] spells;

    private PlayerController player;
    private Camera playerCam;
    private int index = 0;

    public Spell CurrentSpell { get; private set; }

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        playerCam = GetComponentInChildren<Camera>();

        spells = new Spell[3];
        spells[0] = new Force_MoveObject(player);
        spells[1] = new Force_NeutraliseGravity(player);
        spells[2] = new Force_FlipGravity(player);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(spells[index]);
        CurrentSpell = spells[index];

        //Check if Fire1 was pressed
        if (Input.GetAxis("Fire1") == 1)
        {
            if (!spells[index].Properties.passive)
            {
                RaycastHit hit;
                if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxDistance, mask))
                {
                    if (spells[index].Calculate(hit))
                    {
                        if (spells[index].Cast(hit))
                        {

                        }
                    }
                }
            }
            else
            {
                if (spells[index].Calculate())
                {
                    if (spells[index].Cast())
                    {

                    }
                }
            }
        }

        float d = Input.GetAxis("Mouse ScrollWheel");

        if (d > 0f)
        {
            index++;
            OnSpellSwitched();
        }
        else if (d < 0f)
        {
            index--;
            OnSpellSwitched();
        }
            

        if (index < 0)
            index = spells.Length - 1;
        if (index > spells.Length - 1)
            index = 0;
    }

    private void OnSpellSwitched()
    {
        if(SpellSwitched != null)
        {
            SpellSwitched();
        }
    }
}
