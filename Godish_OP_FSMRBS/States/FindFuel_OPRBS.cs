using System;
using UnityEngine;

public class FindFuel_OPRBS : BaseState_OPRBS
{
    private GodishTank_OP_FSMRBS Tank;
    public FindFuel_OPRBS(GodishTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Find Fuel State"] = true;
        Debug.Log("Fuel State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Fuel State"] = false;
        Tank.stats["Fuel Found"] = false;

        return null;
    }

    public override Type StateUpdate()
    {
        if(Tank.checkConsumables("FuelLocation_Loc")){

            Tank.findFuel();

        } else{

            return typeof(Wander_OPRBS);
            
        }

        return null;
    }
}
