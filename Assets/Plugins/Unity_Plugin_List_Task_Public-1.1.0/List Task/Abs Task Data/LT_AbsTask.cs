using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class LT_AbsTask : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract void StartInit();
    
    public abstract bool IsCompletedLogic { get; }
    public abstract event Action OnCompletedLogic;

    public abstract void StartLogic();
}
