using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// В место этого класса может быть любой друго(даже не наследник), просто для примера
/// </summary>
public abstract class TL_ExampleAbsReplicationTask<T> : MonoBehaviour
{
    public abstract bool IsCompletedLogic { get; }
    public abstract event Action OnCompletedLogic;

    public abstract void StartLogic(T data);
}
