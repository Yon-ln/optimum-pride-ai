using System;
using UnityEngine;

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
        Debug.Log("Follow State Entered");
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

        if (Tank.targetTankPosition != null)
        {

            if (Vector3.Distance(Tank.gameObject.transform.position, Tank.targetTankPosition.transform.position) > 25f) 
            {
                Tank.targetTanksFound = null;
                Tank.stats["Enemy Found"] = false;
            }
            else
            {
                return typeof(Shoot_OPRBS);
            }
        }
        
        foreach(var item in Tank.rules.GetRules){
            if(item.CheckRule(Tank.stats) != null){
                return item.CheckRule(Tank.stats);
            }
        }

        return null;
    }
}
