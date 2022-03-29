using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    public Follow_OP(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Follow State"] = true;
        Debug.Log("1");
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

        foreach(var item in Tank.rules.GetRules){
            if(item.CheckRule(Tank.stats) != null){
                return item.CheckRule(Tank.stats);
            }
        }

        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update
        if (Tank.targetTankPosition != null)
        {
            enTankPosition = Tank.targetTankPosition;
            //if the distance is further than 25 set the targets to null and wander again
            if (Vector3.Distance(Tank.gameObject.transform.position, enTankPosition.transform.position) > 25f) 
            {
                Tank.stats["Enemy Found"] = false;
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
