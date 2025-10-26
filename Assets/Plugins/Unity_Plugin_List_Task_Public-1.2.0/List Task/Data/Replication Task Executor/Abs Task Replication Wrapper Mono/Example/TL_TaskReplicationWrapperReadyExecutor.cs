using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// Обертка Monobeh над проекцией
/// В этой реализации следующая задача будет запущена не смотря на то, что прошлая задача еще не закончила свое выполнение
/// </summary>
public class TL_TaskReplicationWrapperReadyExecutor : TL_AbsTaskReplicationWrapperMono
{
    [SerializeField] 
    private TL_ReplicationTaskReadyExecutor _replicationTaskReadyExecutor;
    
    /// <summary>
    /// Собщает id задачи которую пора начинать выполнять
    /// </summary>
    public override event Action<int> OnStartAction
    {
        add
        {
            _replicationTaskReadyExecutor.OnStartAction += value;
        }
        
        remove
        {
            _replicationTaskReadyExecutor.OnStartAction -= value;
        }
    }

    /// <summary>
    /// Проверка выполнена ли задача по указанному id 
    /// </summary>
    public override event Tl_DelegateCheckCompleted.CheckCompleted OnCheckCompleted
    {
        add
        {
            _replicationTaskReadyExecutor.OnCheckCompleted += value;
        }
        
        remove
        {
            _replicationTaskReadyExecutor.OnCheckCompleted -= value;
        }
    }

    
    /// <summary>
    /// Сработает когда одна из задач будет выполнена(укажет id этой задачи) 
    /// </summary>
    public override event Action<int> OnCompletedElement
    {
        add
        {
            _replicationTaskReadyExecutor.OnCompletedElement += value;
        }
        
        remove
        {
            _replicationTaskReadyExecutor.OnCompletedElement -= value;
        }
    }
    

    /// <summary>
    /// Сообщает об успешном выполнении всех указанных задач
    /// </summary>
    public override event Action OnCompleted
    {
        add
        {
            _replicationTaskReadyExecutor.OnCompleted += value;
        }
        
        remove
        {
            _replicationTaskReadyExecutor.OnCompleted -= value;
        }
    }
    public override bool IsCompleted => _replicationTaskReadyExecutor.IsCompleted;

    
    /// <summary>
    /// Запускает логику обработки списка задач
    /// В списке указываю id тех задач, которые еще не выполнены(Completed == false)
    /// </summary>
    public override void StartAction(List<int> listId)
    {
        _replicationTaskReadyExecutor.StartAction(listId);
    }


    /// <summary>
    /// Вызывать когда прийдет ответ от задачи
    /// </summary>
    public override void ActionCompleted()
    {
        _replicationTaskReadyExecutor.ActionCompleted();
    }
    
}