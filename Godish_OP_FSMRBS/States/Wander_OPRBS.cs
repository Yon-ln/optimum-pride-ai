using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wander_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;

    public Wander_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Wander State"] = true;
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
            return typeof(FindFuel_OPRBS);
        }
        else if (loc != null && loc.gameObject.name == "HealthLocation_Loc")
        {
            return typeof(FindHealth_OPRBS);
        }
        else if (loc != null && loc.gameObject.name == "AmmoLocation_Loc")
        {
            return typeof(FindAmmo_OPRBS);
        }
        else
        {
            Tank.Wander();
        }
        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update
        if(Tank.targetTankPosition != null) 
        {
            enTankPosition = Tank.targetTankPosition;
            //if the tank is close than 25 units to the enemy it will start chasing otherwise set the targets to null
            if (Vector3.Distance(Tank.gameObject.transform.position, enTankPosition.transform.position) < 25f)
            {
                return typeof(Follow_OPRBS);
            }
            else
            {
                Tank.targetTanksFound = null;
                enTankPosition = null;
                return null;
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
