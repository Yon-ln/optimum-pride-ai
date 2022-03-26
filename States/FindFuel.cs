using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFuel : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public FindFuel(SmartTank_OP_FSMRBS tank)
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
        return null;
    }
}
