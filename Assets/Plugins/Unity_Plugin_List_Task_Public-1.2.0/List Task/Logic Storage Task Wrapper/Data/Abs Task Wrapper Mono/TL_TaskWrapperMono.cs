using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обертка в виде Monobeh над проекцией логики выполнения задачи
/// Нужен, что бы можно было подменять проекции
///
/// Недостаток такого подхода в том, что если какой либо Task в аргументы нужно будет предать,
/// то для этого нужно будет создать не только абстракцию Task, но и продублировать всю эту цепочку
/// с логикой обертки над действиями(да и сами действия прийдется переписывать, т.к в них тоже указ. тип Task)
/// </summary>
public abstract class TL_TaskWrapperMono : MonoBehaviour
{
    public abstract List<LT_AbsTaskWrapperDefault> ListAction { get; }
    
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract void StartInit();
    
    public abstract event Action OnCompleted;
    public abstract bool IsCompleted { get; }

    public abstract void StartAction();
    
}
