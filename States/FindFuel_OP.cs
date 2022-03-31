using System;
using UnityEngine;

public class FindFuel_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    public FindFuel_OP(SmartTank_OP_FSMRBS tank)
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

            return typeof(Wander_OP);
            
        }

        return null;
    }
}
