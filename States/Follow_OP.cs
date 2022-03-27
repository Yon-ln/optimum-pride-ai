using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Follow_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;
    public Follow_OP(GodishTank_OP_FSM tank)
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
