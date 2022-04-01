using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FSM_OPRBS : MonoBehaviour //Rename the inheritance if pushing to main and your script name is different
{
    private Dictionary<Type, BaseState_OPRBS> states;
    public BaseState_OPRBS currentState;

    public BaseState_OPRBS CurrentState 
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

    public void setStates(Dictionary<Type, BaseState_OPRBS> states) 
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
