using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine_OP_FSM : MonoBehaviour
{
    private Dictionary<Type, BaseState_OP> states;

    public BaseState_OP currentState;
    public BaseState_OP CurrentState {
        get{ return currentState; }
        private set{ currentState = value; }
    }

    public void SetStates(Dictionary<Type, BaseState_OP> states){
        this.states = states;
    }

    private void Update()
    {
        if(CurrentState == null){
            CurrentState = states.Values.First();
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
