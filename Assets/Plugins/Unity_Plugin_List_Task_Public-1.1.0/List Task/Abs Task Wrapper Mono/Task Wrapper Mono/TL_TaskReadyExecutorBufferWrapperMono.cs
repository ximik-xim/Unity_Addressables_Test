using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TL_TaskReadyExecutorBufferWrapperMono : TL_TaskWrapperMono
{
    [SerializeField] 
    private TL_TaskReadyExecutorBuffer _taskExecutor;

    public override List<LT_AbsTask> ListAction => _taskExecutor._listAction;
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
