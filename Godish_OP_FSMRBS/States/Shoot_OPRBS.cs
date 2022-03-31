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
        Tank.stats["Turret Shot"] = false;

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
            if (Vector3.Distance(Tank.gameObject.transform.position, Tank.targetTankPosition.transform.position) > 25f)
            {
                Tank.targetTanksFound = null;
                Tank.stats["Enemy Found"] = false;
            }
            else
            {
                Tank.stats["Turret Shot"] = true;
                Tank.shoot();
            }

        }

        foreach (var item in Tank.rules.GetRules)
        {
            if (item.CheckRule(Tank.stats) != null)
            {
                return item.CheckRule(Tank.stats);
            }
        }

        return null;
    }
}
