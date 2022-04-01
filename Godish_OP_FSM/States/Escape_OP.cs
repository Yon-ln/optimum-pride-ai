using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_OP : BaseState_OP
{
    private GodishTank_OP_FSM Tank;//Change the script type eventually temporary name lol
    private float timer = 0.0f;
    public Escape_OP(GodishTank_OP_FSM tank) 
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        timer = 0;
        return null;
    }

    public override Type StateExit()
    {
        timer = 0;
        return null;
    }

    public override Type StateUpdate()
    {
        timer += Time.deltaTime;
        if (Tank.checkFuel() < 50f)
        {
            return typeof(FindFuel_OP);
        }
        else if (Tank.checkHealth() < 50f)
        {
            return typeof(FindHealth_OP);
        }
        else if (Tank.checkAmmo() == 0)
        {
            return typeof(FindAmmo_OP);
        }
        else if (timer > 7)
        {
            timer = 0;
            Debug.Log("Wander started");
            return typeof(Wander_OP);
        }
        else
        {
            Tank.targetTankPosition = null;
            Tank.escape();
        }
        GameObject enTankPosition;//gets tank position if there is a tank so that it doesn't call an error on every update

        return null;
    }
}
