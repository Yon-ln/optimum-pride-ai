using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Follow_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    public Follow_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Follow State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Follow State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        Tank.Follow();
        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update
        if (Tank.targetTankPosition != null)
        {
            enTankPosition = Tank.targetTankPosition;
            //if the distance is further than 25 set the targets to null and wander again
            if (Vector3.Distance(Tank.gameObject.transform.position, enTankPosition.transform.position) > 25f) 
            {
                Tank.targetTanksFound = null;
                enTankPosition = null;
                return typeof(Wander_OP);
            }
            else
            {
                return typeof(Shoot_OP);
            }
        }
        return null;
    }
}
