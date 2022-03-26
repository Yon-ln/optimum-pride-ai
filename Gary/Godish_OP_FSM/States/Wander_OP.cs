using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander_OP : BaseState
{
    private GodishTank_OP_FSM Tank;

    public Wander_OP(GodishTank_OP_FSM tank)
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
        Tank.Wander();
        return null;
    }
}
