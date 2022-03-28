using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    public Shoot_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Shoot State"] = true;
        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Shoot State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        //Robbie
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
                Tank.shoot();
                return typeof(Wander_OP);
            }
        }
        return null;
    }
}
