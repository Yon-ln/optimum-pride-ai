using System;
using UnityEngine;

public class Shoot_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    private float time = 0;
    public Shoot_OP(SmartTank_OP_FSMRBS tank)
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

        foreach(var item in Tank.rules.GetRules){
            if(item.CheckRule(Tank.stats) != null){
                return item.CheckRule(Tank.stats);
            }
        }

        return null;
        
    }
}
