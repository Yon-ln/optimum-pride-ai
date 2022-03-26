using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public Follow(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Follow State"] = true;

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Follow State"] = false;

        return null;
    }

    public override Type StateUpdate()
    {
        return null;
    }
}
