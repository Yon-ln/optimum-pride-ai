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
        Debug.Log("3");

        return null;
    }

    public override Type StateExit()
    {
        Tank.stats["Find Ammo State"] = false;
        return null;
    }

    public override Type StateUpdate()
    {
        bool Anyammo = false;
        foreach (GameObject item in Tank.potConsumableLocation)
        {
            if (item.transform.name == "AmmoLocation_Loc")
            {
                Anyammo = true;
            }
        }
        
        if (Anyammo)
        {
            
            Tank.findAmmo();
            return null;
        }
        else
        {
            return typeof(Wander_OP);
        }
    }
}
