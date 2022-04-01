using System;
using UnityEngine;

public class Escape_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;

    public Escape_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Escape State"] = true;
        Tank.stats["Strafing"] = true;

        Vector3 tankPosition = Tank.transform.position;
        Vector3 enemyPosition = Tank.targetTankPosition.transform.position;

        float perpendicular = -1.0f / ((enemyPosition.z - tankPosition.z) / (enemyPosition.x - tankPosition.x));

        Tank.strafePositions[0] = tankPosition + new Vector3(8.0f ,0.0f, 8.0f * perpendicular);
        Tank.strafePositions[1] = tankPosition + new Vector3(-8.0f ,0.0f, -8.0f * perpendicular);
        
        Debug.Log("Escape State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Escape State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        if(Tank.stats["Low Health"] || Tank.stats["Low Fuel"] || Tank.stats["Low Ammo"]){
            Tank.stats["Strafing"] = false;
            return typeof(Wander_OPRBS);
        }

        if(Tank.stats["Strafing"]){
            Tank.Strafe();

        } else{
            return typeof(Shoot_OPRBS);
        }

        return null;
    }
}
