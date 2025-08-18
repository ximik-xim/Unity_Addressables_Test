using System;
using UnityEngine;

public abstract class TL_TaskReplicationWrapperMono : MonoBehaviour
{
    public abstract event Action OnCompleted;
    public abstract bool IsCompleted { get; }

    public abstract void StartAction();
}
