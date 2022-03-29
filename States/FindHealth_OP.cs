using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    public FindHealth_OP(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Find Health State"] = true;
        Debug.Log("5");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Health State"] = false;
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
