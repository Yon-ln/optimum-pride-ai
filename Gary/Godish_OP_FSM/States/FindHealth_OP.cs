using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth_OP : BaseState
{
    private GodishTank_OP_FSM Tank;
    public FindHealth_OP(GodishTank_OP_FSM tank)
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
