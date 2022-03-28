using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFuel_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    public FindFuel_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Find Fuel State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Fuel State"] = false;
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
