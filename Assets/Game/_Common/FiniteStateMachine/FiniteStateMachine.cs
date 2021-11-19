using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private IState currentState;

    private Dictionary<Type, IState> states;
    //private Dictionary<string, State> statesNames; // allow for reusable states

    private Type currentStateId;

    public FiniteStateMachine()
    {
        states = new Dictionary<Type, IState>();
    }

    /// <summary>
    /// This method allows using types in ChangeState. Eg: ChangeState(typeof(CustomState));
    /// </summary>
    /// <param name="state"></param>
    public void AddState(IState state)
    {
        states[state.GetType()] = state;
        state.SetFSM(this);
    }

    //public void AddState(string stateName, State state)
    //{
    //    statesNames[stateName] = state;
    //    states[state.GetType()] = state; // This might override an state if using the same type
    //    state.SetFSM(this);
    //}

    //public void ChangeState(string stateName)
    //{
    //    if (statesNames.ContainsKey(stateName))
    //    {
    //        ChangeState(statesNames[stateName]);
    //    }
    //}

    public void ChangeState( IState state)
    {
        //Debug.Log($"Changing to {state}");
        if (state != currentState)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }

        if (state == null)
            Debug.LogError("new state should not be null");
    }

    public void ChangeState(Type state)
    {
        if (states.ContainsKey(state))
        {
            ChangeState(states[state]);            
        }
        else
        {
            Debug.Log("State " + state + " not found");
        }

        currentStateId = state.GetType();
    }

    public void ChangeState<t>()
    {
        ChangeState(typeof(t));
    }

    public Type GetStateID()
    {
        return currentStateId;
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    internal IState GetCurrentState()
    {
        return currentState;
    }
}
