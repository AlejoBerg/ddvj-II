using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState<T>
{
    Dictionary<T, FSMState<T>> _myDictionaryTransitions = new Dictionary<T, FSMState<T>>();

    public virtual void Awake() {}

    public virtual void Execute() {}

    public virtual void Sleep() {}

    public void AddTransition(T transitionKey, FSMState<T> newTransition)
    {
        if (!_myDictionaryTransitions.ContainsKey(transitionKey))
        {
            _myDictionaryTransitions.Add(transitionKey, newTransition);
        }
    }

    public void RemoveTransition(T keyTransitionToRemove)
    {
        if (_myDictionaryTransitions.ContainsKey(keyTransitionToRemove))
        {
            _myDictionaryTransitions.Remove(keyTransitionToRemove);
        }
    }

    public FSMState<T> GetTransition(T input) //Pido el estado que contiene este input
    {
        if (_myDictionaryTransitions.ContainsKey(input))
        {
            return _myDictionaryTransitions[input];              
        }

        return null;
    }
}
