using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState<T> : FSMState<T>
{
    private IEntity _entity;

    public IdleState(IEntity entity)
    {
        _entity = entity;
    }

    public override void Execute()
    {
        Debug.Log("IdleState - Execute");
        _entity.Idle();
    }
}
