using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public FindHealth(SmartTank_OP_FSMRBS tank)
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
        return null;
    }
}
