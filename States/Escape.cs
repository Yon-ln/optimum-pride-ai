using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public Escape(SmartTank_OP_FSMRBS tank) 
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
        return null;
    }
}
