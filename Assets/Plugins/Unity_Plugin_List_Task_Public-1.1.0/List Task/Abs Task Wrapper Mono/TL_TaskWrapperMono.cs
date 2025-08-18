using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TL_TaskWrapperMono : MonoBehaviour
{
    public abstract List<LT_AbsTask> ListAction { get; }
    
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract void StartInit();
    
    public abstract event Action OnCompleted;
    public abstract bool IsCompleted { get; }

    public abstract void StartAction();
    
}
