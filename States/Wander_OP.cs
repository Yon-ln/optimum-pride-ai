using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    public Wander_OP(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Wander State"] = true;
        Debug.Log("2");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Wander State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        float lowestdist = 1000;
        GameObject loc = null;

        foreach(GameObject item in Tank.potConsumableLocation) 
        {
            if (Vector3.Distance(Tank.transform.position, item.transform.position) < lowestdist)
            {
                lowestdist = Vector3.Distance(Tank.transform.position, item.transform.position);
                loc = item;
            }
        }
        if (loc != null && loc.gameObject.name == "FuelLocation_Loc")
        {
            return typeof(FindFuel_OP);
        }
        else if (loc != null && loc.gameObject.name == "HealthLocation_Loc")
        {
            return typeof(FindHealth_OP);
        }
        else if (loc != null && loc.gameObject.name == "AmmoLocation_Loc")
        {
            return typeof(FindAmmo_OP);
        }
        else
        {
            Tank.Wander();
        }
        
        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update
        if(Tank.targetTankPosition != null) 
        {
            Tank.stats["Enemy Found"] = true;
            enTankPosition = Tank.targetTankPosition;
            //if the tank is close than 25 units to the enemy it will start chasing otherwise set the targets to null
            if (Vector3.Distance(Tank.gameObject.transform.position, enTankPosition.transform.position) < 25f)
            {
                Tank.stats["Enemy Nearby"] = true;
                return typeof(Follow_OP);
            }
            else
            {
                Tank.stats["Enemy Nearby"] = false;
                Tank.targetTanksFound = null;
                enTankPosition = null;
                return null;
            }
        }
        return null;
    }
}
