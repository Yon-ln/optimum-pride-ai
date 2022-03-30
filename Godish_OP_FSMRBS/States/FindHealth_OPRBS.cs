using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    public FindHealth_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Find Health State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Health State"] = false;
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

        if (Tank.stats["Health Found"] == true)
        {
            Tank.findHealth();
            return null;
        }
        else
        {
            return typeof(Wander_OP);
        }
    }
}
