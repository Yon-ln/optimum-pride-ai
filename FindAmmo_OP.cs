using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAmmo_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;
    public FindAmmo_OP(GodishTank_OP_FSM tank)
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
        Tank.findAmmo();
        return null;
    }
}
