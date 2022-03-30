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
        foreach (var item in Tank.rules.GetRules)
        {
            if (item.CheckRule(Tank.stats) != null)
            {
                return item.CheckRule(Tank.stats);
            }
        }

        if (Tank.stats["Fuel Found"] == true) 
        {
            Tank.findFuel();
            return null;
        }
        else 
        {
            return typeof(Wander_OPRBS);
        }
    }
}
