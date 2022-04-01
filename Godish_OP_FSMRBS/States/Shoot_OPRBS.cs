using System;
using UnityEngine;

public class Shoot_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    private float time = 0;
    public Shoot_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Shoot State"] = true;
        Tank.stats["Turret Shot"] = false;

        Debug.Log("Shoot State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Shoot State"] = false;
        time = 0.0f;
        return null;
    }

    public override Type StateUpdate()
    {
        time += Time.deltaTime;
        
        //Robbie
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
        else if (Tank.basePosition != null)
        {
            Tank.ShootBase();
            Tank.stats["Enemy Base Found"] = false;
            Tank.stats["Turret Shot"] = true;
            Tank.basePosition = null;
            return typeof(Wander_OPRBS);
        }

        foreach (var item in Tank.rules.GetRules){
            if(item.CheckRule(Tank.stats) != null){
                return item.CheckRule(Tank.stats);
            }
        }

        return null;
        
    }
}
