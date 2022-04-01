using System;
using UnityEngine;

public class Escape_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    private Transform initialTankPosition;

    public Escape_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Escape State"] = true;
        Tank.stats["Strafing"] = true;

        initialTankPosition = Tank.transform;

        if(Tank.targetTankPosition != null){
            Transform enemyPosition = Tank.targetTankPosition.transform.Find("Model").transform.Find("Turret").transform;

            Tank.strafePositions[0] = initialTankPosition.position + enemyPosition.right * 15.0f;
            Tank.strafePositions[1] = initialTankPosition.position + enemyPosition.right * -15.0f;

        } else{
            Tank.strafePositions[0] = initialTankPosition.position;
            Tank.strafePositions[1] = initialTankPosition.position;
        }


        Debug.Log("Escape State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Escape State"] = false;

        if(Tank.strafeIndex == 0) Tank.strafeIndex = 1;
        else Tank.strafeIndex = 0;

        return null;
    }

    public override Type StateUpdate()
    {   
        if(Tank.stats["Low Health"] || Tank.stats["Low Fuel"] || Tank.stats["Low Ammo"]){
            Tank.stats["Strafing"] = false;
            return typeof(Wander_OPRBS);
        }

   
        if (Vector3.Distance(Tank.gameObject.transform.position, Tank.targetTankPosition.transform.position) > 25f)
        {   
            Tank.targetTanksFound = null;
            Tank.stats["Enemy Found"] = false;
            return typeof(Wander_OPRBS);
        }
        else
        {
            if(Tank.stats["Strafing"]){

                if(Vector3.Distance(Tank.strafePositions[Tank.strafeIndex], Tank.transform.position) < 5.0f) { 
                    Tank.stats["Strafing"] = false;
                    return typeof(Shoot_OPRBS);
                }

                Tank.Strafe();
            } else{

                return typeof(Shoot_OPRBS);
            }
        }
 



        return null;
    }
}