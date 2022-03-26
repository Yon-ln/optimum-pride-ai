using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public Wander(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Wander State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Wander State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        return null;
    }
}
