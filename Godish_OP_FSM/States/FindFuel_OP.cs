using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFuel_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;
    public FindFuel_OP(GodishTank_OP_FSM tank)
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
        bool Anyfuel = false;
        foreach (GameObject item in Tank.potConsumableLocation)
        {
            if (item.transform.name == "FuelLocation_Loc")
            {
                Anyfuel = true;
            }
        }
        if (Anyfuel) 
        {
            Tank.findFuel();
            return null;
        }
        else 
        {
            return typeof(Wander_OP);
        }
    }
}
