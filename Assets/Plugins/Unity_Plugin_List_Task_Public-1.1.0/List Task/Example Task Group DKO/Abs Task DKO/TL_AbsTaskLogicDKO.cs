using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Наследники будут выполнять какую то задачу, по окончанию которой вызовут event
/// Так же, наследники должны сами инициализироваться (желательно в Awake)
/// </summary>
public abstract class TL_AbsTaskLogicDKO : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract event Action OnCompletedLogic;
    public abstract bool IsCompletedLogic { get; }

    public abstract void StartLogic(DKOKeyAndTargetAction dataDKO);
}
