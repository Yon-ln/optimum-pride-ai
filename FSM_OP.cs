using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseState_OP 
{
    public abstract Type StateUpdate();
    public abstract Type StateEnter();
    public abstract Type StateExit();
}

public class FSM_OP : MonoBehaviour //Rename the inheritance if pushing to main and your script name is different
{
    private Dictionary<Type, BaseState_OP> states;
    public BaseState_OP currentState;

    public BaseState_OP CurrentState 
    {
        get 
        {
            return currentState;
        }
        set 
        {
            currentState = value;
        }
    }

    public void setStates(Dictionary<Type, BaseState_OP> states) 
    {
        this.states = states;
    }

    private void Update()
    {
        if(CurrentState == null) 
        {
            CurrentState = states.Values.First();
        }
        else 
        {
            var nextState = CurrentState.StateUpdate();
            if(nextState != null && nextState != CurrentState.GetType()) 
            {
                SwitchToState(nextState);
            }
        }
    }

    void SwitchToState(Type nextState) 
    {
        CurrentState.StateExit();
        CurrentState = states[nextState];
        CurrentState.StateEnter();
    }
};
