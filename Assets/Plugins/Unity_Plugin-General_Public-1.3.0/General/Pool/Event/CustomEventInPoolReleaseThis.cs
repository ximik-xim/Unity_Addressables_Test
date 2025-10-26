using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфеис, который может быть у обьекта пула
/// Нужен в случае, если обьект будет сам себя возращать в пулл 
/// (его реализовать, должен тот класс, который используеться в обобщении пула)
/// </summary>
public interface CustomEventInPoolReleaseThis<T>
{
    public event Action<T> OnReleaseThis;
}
