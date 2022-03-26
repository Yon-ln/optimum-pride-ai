using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAmmo : BaseState
{
    private SmartTank_OP_FSMRBS Tank;
    public FindAmmo(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        return null;
    }

    public override Type StateExit()
    {
        return null;
    }

    public override Type StateUpdate()
    {
        return null;
    }
}
