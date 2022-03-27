using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAmmo_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;
    public FindAmmo_OP(GodishTank_OP_FSM tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        return null;
    }

    public override Type StateExit()
    {
        return null;
    }

    public override Type StateUpdate()
    {
        bool Anyammo = false;
        foreach (GameObject item in Tank.potConsumableLocation)
        {
            if (item.transform.name == "AmmoLocation_Loc")
            {
                Anyammo = true;
            }
        }
        if (Anyammo)
        {
            Tank.findAmmo();
            return null;
        }
        else
        {
            return typeof(Wander_OP);
        }
    }
}
