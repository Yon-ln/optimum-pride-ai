using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    public FindHealth_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Find Health State"] = true;
        Debug.Log("Health State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Health State"] = false;
        Tank.stats["Health Found"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        if(Tank.checkConsumables("HealthLocation_Loc")){

            Tank.findHealth();

        } else{

            return typeof(Wander_OPRBS);
            
        }

        return null;
    }
}
