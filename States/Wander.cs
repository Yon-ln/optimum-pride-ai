using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : BaseState
{
    private Godish Tank;
    public Wander(Godish tank)
    {
        this.Tank = tank;
    }

    public override Type StateEnter()
    {
        return null;
    }

    public override Type StateExit()
    {
        return null;
    }

    public override Type StateUpdate()
    {
        return null;
    }
}
