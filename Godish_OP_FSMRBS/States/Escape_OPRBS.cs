using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;//Change the script type eventually temporary name lol
    private float timer = 0.0f;
    public Escape_OPRBS(GodishTank_OP_FSMRBS tank) 
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Escape State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Escape State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        Tank.escape();
        timer = +Time.deltaTime;
        if(timer > 5) 
        {
            timer = 0;
            return typeof(Wander_OPRBS);
        }

        foreach (var item in Tank.rules.GetRules)
        {
            if (item.CheckRule(Tank.stats) != null)
            {
                return item.CheckRule(Tank.stats);
            }
        }
        return null;
    }
}
