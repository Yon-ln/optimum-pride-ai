using System;
using UnityEngine;

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
        Debug.Log("Wander State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Wander State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        Tank.Wander();

        foreach(GameObject item in Tank.potConsumableLocation) 
        {   
            if(item != null){
                if (item.name == "FuelLocation_Loc"){
                    Tank.stats["Fuel Found"] = true;
                } else if(item.name == "HealthLocation_Loc"){
                    Tank.stats["Health Found"] = true;
                } else if(item.name == "AmmoLocation_Loc"){
                    Tank.stats["Ammo Found"] = true;
                }
            }
        }

        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update
        if(Tank.targetTankPosition != null) 
        {
            
            enTankPosition = Tank.targetTankPosition;
            //if the tank is close than 25 units to the enemy it will start chasing otherwise set the targets to null
            if (Vector3.Distance(Tank.gameObject.transform.position, enTankPosition.transform.position) < 25f)
            {
                Tank.stats["Enemy Found"] = true;

            }
            else
            {
                Tank.stats["Enemy Found"] = false;
                Tank.targetTanksFound = null;
                enTankPosition = null;
                
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
