using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander_OP : BaseState_OP
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
        if (Tank.checkFuel() < 20f)
        {
            return typeof(FindFuel_OP);
        }
        else if(Tank.checkHealth() < 20f)
        {
            return typeof(FindHealth_OP);
        }
        else if (Tank.checkFuel() < 20f)
        {
            return typeof(FindFuel_OP);
        }
        else
        {
            Tank.Wander();
        }
        
        return null;
    }
}
