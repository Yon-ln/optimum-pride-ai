using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public Shoot(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Shoot State"] = true;

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Shoot State"] = false;

        return null;
    }

    public override Type StateUpdate()
    {
        return null;
    }
}
