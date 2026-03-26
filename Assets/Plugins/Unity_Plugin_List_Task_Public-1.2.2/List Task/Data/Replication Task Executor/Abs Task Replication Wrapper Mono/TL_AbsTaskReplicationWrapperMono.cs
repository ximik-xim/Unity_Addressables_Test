using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Служит оберткой, для удобной подмены скриптов через инспектор
/// </summary>
public abstract class TL_AbsTaskReplicationWrapperMono : MonoBehaviour
{

    /// <summary>
    /// Собщает id задачи которую пора начинать выполнять
    /// </summary>
    public abstract event Action<int> OnStartAction;
    
    /// <summary>
    /// Проверка выполнена ли задача по указанному id 
    /// </summary>
    public abstract event Tl_DelegateCheckCompleted.CheckCompleted OnCheckCompleted;

    /// <summary>
    /// Сработает когда одна из задач будет выполнена(укажет id этой задачи) 
    /// </summary>
    public abstract event Action<int> OnCompletedElement;
    
    /// <summary>
    /// Сообщает об успешном выполнении всех указанных задач
    /// </summary>
    public abstract event Action OnCompleted;

    public abstract bool IsCompleted { get; }


    /// <summary>
    /// Запускает логику обработки списка задач
    /// В списке указываю id тех задач, которые еще не выполнены(Completed == false)
    /// </summary>
    public abstract void StartAction(List<int> listId);

    /// <summary>
    /// Вызывать когда прийдет ответ от задачи
    /// </summary>
    public abstract void ActionCompleted();

}
