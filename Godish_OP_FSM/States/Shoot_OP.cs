using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;
    public Shoot_OP(GodishTank_OP_FSM tank)
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
                Debug.Log("Optimus has opened fire!");
                Tank.shoot();
                return typeof(Escape_OP);
            }
        }
        return null;
    }
}
