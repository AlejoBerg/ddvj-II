using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState<T> : FSMState<T>
{
    private IEntity _entity;

    public WalkState(IEntity entity)
    {
        _entity = entity;
    }

    public override void Execute()
    {
        Debug.Log("WalkState - Execute");
        _entity.Move();
    }
}
