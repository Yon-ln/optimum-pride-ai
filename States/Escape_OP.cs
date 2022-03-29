using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    public Escape_OP(SmartTank_OP_FSMRBS tank) 
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Escape State"] = true;
        Debug.Log("4");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Escape State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        foreach(var item in Tank.rules.GetRules){
            if(item.CheckRule(Tank.stats) != null){
                return item.CheckRule(Tank.stats);
            }
        }

        return null;
    }
}
