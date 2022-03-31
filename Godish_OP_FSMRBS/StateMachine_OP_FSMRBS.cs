using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine_OP_FSMRBS : MonoBehaviour
{
    private Dictionary<Type, BaseState_OPRBS> states;

    public BaseState_OPRBS currentState;
    public BaseState_OPRBS CurrentState {
        get{ return currentState; }
        private set{ currentState = value; }
    }

    public void SetStates(Dictionary<Type, BaseState_OPRBS> states){
        this.states = states;
    }

    private void Update()
    {
        if(CurrentState == null){
            CurrentState = states.Values.First();
            CurrentState.StateEnter();
        } else{
            var nextState = CurrentState.StateUpdate();
            if(nextState != null && nextState != CurrentState.GetType()){
                SwitchToState(nextState);
            }
        }
    }

    void SwitchToState(Type nextState){
        CurrentState.StateExit();
        CurrentState = states[nextState];
        CurrentState.StateEnter();
    }
}
