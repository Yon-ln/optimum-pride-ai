using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth : BaseState
{
    private Godish Tank;
    public FindHealth(Godish tank)
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
