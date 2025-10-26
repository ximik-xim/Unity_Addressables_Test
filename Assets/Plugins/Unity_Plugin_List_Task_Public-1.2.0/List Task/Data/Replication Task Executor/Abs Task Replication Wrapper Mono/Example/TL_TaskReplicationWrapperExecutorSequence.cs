using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обертка Monobeh над проекцией
/// В этой реализации следующая задача будет запущена только после завершение выполнения предыдущей задачи
/// </summary>
public class TL_TaskReplicationWrapperExecutorSequence : TL_AbsTaskReplicationWrapperMono
{
    [SerializeField] 
    private TL_ReplicationTaskExecutorSequence _replicationTaskExecutorSequence;
    
    /// <summary>
    /// Собщает id задачи которую пора начинать выполнять
    /// </summary>
    public override event Action<int> OnStartAction
    {
        add
        {
            _replicationTaskExecutorSequence.OnStartAction += value;
        }
        
        remove
        {
            _replicationTaskExecutorSequence.OnStartAction -= value;
        }
    }

    /// <summary>
    /// Проверка выполнена ли задача по указанному id 
    /// </summary>
    public override event Tl_DelegateCheckCompleted.CheckCompleted OnCheckCompleted
    {
        add
        {
            _replicationTaskExecutorSequence.OnCheckCompleted += value;
        }
        
        remove
        {
            _replicationTaskExecutorSequence.OnCheckCompleted -= value;
        }
    }

    
    /// <summary>
    /// Сработает когда одна из задач будет выполнена(укажет id этой задачи) 
    /// </summary>
    public override event Action<int> OnCompletedElement
    {
        add
        {
            _replicationTaskExecutorSequence.OnCompletedElement += value;
        }
        
        remove
        {
            _replicationTaskExecutorSequence.OnCompletedElement -= value;
        }
    }
    

    /// <summary>
    /// Сообщает об успешном выполнении всех указанных задач
    /// </summary>
    public override event Action OnCompleted
    {
        add
        {
            _replicationTaskExecutorSequence.OnCompleted += value;
        }
        
        remove
        {
            _replicationTaskExecutorSequence.OnCompleted -= value;
        }
    }
    public override bool IsCompleted => _replicationTaskExecutorSequence.IsCompleted;

    
    /// <summary>
    /// Запускает логику обработки списка задач
    /// В списке указываю id тех задач, которые еще не выполнены(Completed == false)
    /// </summary>
    public override void StartAction(List<int> listId)
    {
        _replicationTaskExecutorSequence.StartAction(listId);
    }


    /// <summary>
    /// Вызывать когда прийдет ответ от задачи
    /// </summary>
    public override void ActionCompleted()
    {
        _replicationTaskExecutorSequence.ActionCompleted();
    }

}
