using System;
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
                return typeof(Shoot_OP);
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
