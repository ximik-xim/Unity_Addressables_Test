using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обертка над проекцией, которая
/// выполняеться по цепочке, друг за другом, дожидаясь окончания выполнения опереции у текущего элемента и только потом переходя к следующему
/// </summary>
public class TL_TaskExecutorSequenceWrapperMono : TL_TaskWrapperMono
{
    [SerializeField] 
    private TL_TaskExecutorSequence _taskExecutor;

    public override List<LT_AbsTaskWrapperDefault> ListAction => _taskExecutor._listAction;
    public override event Action OnInit
    {
        add
        {
            _taskExecutor.OnInit += value;
        }

        remove
        {
            _taskExecutor.OnInit -= value;
        }
    }

    public override bool IsInit => _taskExecutor.IsInit;
    public override void StartInit()
    {
         _taskExecutor.StartInit();
    }

    public override event Action OnCompleted
    {
        add
        {
            _taskExecutor.OnCompleted += value;
        }

        remove
        {
            _taskExecutor.OnCompleted -= value;
        }
    }

    public override bool IsCompleted => _taskExecutor.IsCompleted;
    public override void StartAction()
    {
         _taskExecutor.StartAction();
    }
}
