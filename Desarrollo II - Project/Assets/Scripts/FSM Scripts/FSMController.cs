using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController<T>
{
    private FSMState<T> _currentState;

    public void SetInitialState(FSMState<T> _initialState)
    {
        _currentState = _initialState;
        _currentState.Awake();
    }

    public void OnUpdate()
    {
        _currentState.Execute();
    }

    public void MakeTransition(T stateToTransition)
    {
        FSMState<T> newState = _currentState.GetTransition(stateToTransition);
        if (newState == null) return;

        _currentState.Sleep();
        _currentState = newState;
        _currentState.Awake();
    }
}
