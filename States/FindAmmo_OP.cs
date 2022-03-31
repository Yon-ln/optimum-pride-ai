using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAmmo_OP : BaseState_OP
{
    private SmartTank_OP_FSMRBS Tank;
    public FindAmmo_OP(SmartTank_OP_FSMRBS tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        Tank.stats["Find Ammo State"] = true;
        Debug.Log("Ammo State Entered");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Ammo State"] = false;
        Tank.stats["Ammo Found"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        if(Tank.checkConsumables("AmmoLocation_Loc")){

            Tank.findAmmo();

        } else{

            return typeof(Wander_OP);
            
        }

        return null;
    }
}
