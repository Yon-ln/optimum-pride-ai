using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;//Change the script type eventually temporary name lol
    public Escape_OPRBS(GodishTank_OP_FSMRBS tank) 
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Escape State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Escape State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        return null;
    }
}
