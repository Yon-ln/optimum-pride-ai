using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState_OPRBS
{
    public abstract Type StateUpdate();
    public abstract Type StateEnter();
    public abstract Type StateExit();    
}
