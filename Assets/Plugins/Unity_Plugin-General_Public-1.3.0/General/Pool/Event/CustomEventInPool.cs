using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфеис, который может быть у обьекта пула
/// Нужен, что бы сообщать обьекту из пула об различных event 
/// (его реализовать, должен тот класс, который используеться в обобщении пула)
/// </summary>
public interface CustomEventInPool
{
    /// <summary>
    /// Сработает до event взятия обьета из пула(OnGetObject)
    /// </summary>
    public void OnBeforeGetObject();
    /// <summary>
    /// Сработает после event взятия обьета из пула(OnGetObject)
    /// </summary>
    public void OnAfterGetObject();

    /// <summary>
    /// Сработает до event пула об возращении обьекта в пулл(OnReleaseObject)
    /// </summary>
    public void OnBeforeReleaseObject();
    /// <summary>
    /// Сработает после event пула об возращении обьекта в пулл(OnReleaseObject)
    /// </summary>
    public void OnAfterReleaseObject();
    
}
