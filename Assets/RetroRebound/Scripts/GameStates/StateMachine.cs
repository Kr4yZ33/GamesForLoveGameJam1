using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class StateMachine : MonoBehaviour
{

    public Dictionary<Type, BaseState> _availableStates;
    public BaseState CurrentState { get; set; }
    public BaseState nextState;
    

    public void SetState(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }

    // Update is called once per frame
    private void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
            CurrentState.Tick_Enter();
        }
        if (nextState == null)
        {
            //Look for input based on current state 
            CurrentState.Tick_Execute();
        }
    }
    public void SwitchToNewState(BaseState nextState)
    {
        CurrentState.Tick_Exit();
        CurrentState = nextState;
        CurrentState.Tick_Enter();
        nextState = null;
    }
}
