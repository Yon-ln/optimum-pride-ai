using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        Tank.Wander();
        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update
        if(Tank.targetTankPosition != null) 
        {
            enTankPosition = Tank.targetTankPosition;
            //if the tank is close than 25 units to the enemy it will start chasing otherwise set the targets to null
            if (Vector3.Distance(Tank.gameObject.transform.position, enTankPosition.transform.position) < 25f)
            {
                return typeof(Follow_OP);
            }
            else
            {
                Tank.targetTanksFound = null;
                enTankPosition = null;
                return null;
            }
        }
        return null;
    }
}
