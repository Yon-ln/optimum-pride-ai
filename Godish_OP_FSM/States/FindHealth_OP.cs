using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth_OP : BaseState_OP
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
        bool Anyhealth = false;
        foreach (GameObject item in Tank.potConsumableLocation)
        {
            if (item.transform.name == "HealthLocation_Loc")
            {
                Anyhealth = true;
            }
        }
        if (Anyhealth)
        {
            Tank.findHealth();
            return null;
        }
        else
        {
            return typeof(Wander_OP);
        }
    }
}
