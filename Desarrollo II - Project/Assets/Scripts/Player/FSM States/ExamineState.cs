using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineState<T> : FSMState<T>
{
    private IEntityPlayer _playerEntity;

    public ExamineState(IEntityPlayer playerEntity)
    {
        _playerEntity = playerEntity;
    }

    public override void Execute()
    {
        Debug.Log("ExamineState - Execute");
        _playerEntity.Examine();
    }
}
