using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обертка над проекцией, которая
/// Запускаются все задачи, без ожидания окончания предыдущей задачи.
/// По итогу порядок выполнения задач может быть любой, в этой реализ. важен сам факт того,
/// что все задачи были выполнены
///
/// В этой реализации используется буфер находящийся внутри метода
/// </summary>
public class TL_TaskReadyExecutorLocalBufferWrapperMono : TL_TaskWrapperMono
{
    [SerializeField] 
    private TL_TaskReadyExecutorLocalBuffer _taskExecutor;

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
