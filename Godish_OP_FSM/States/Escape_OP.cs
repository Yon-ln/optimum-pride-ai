using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;//Change the script type eventually temporary name lol
    public Escape_OP(GodishTank_OP_FSM tank) 
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
