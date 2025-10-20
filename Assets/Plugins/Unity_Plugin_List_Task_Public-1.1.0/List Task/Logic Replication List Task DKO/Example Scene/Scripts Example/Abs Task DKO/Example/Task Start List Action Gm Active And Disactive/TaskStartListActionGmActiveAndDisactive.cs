using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Запустит логику для включение или отключения GameObject
/// </summary>
public class TaskStartListActionGmActiveAndDisactive : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic
    {
        add
        {
            _listActionGmActiveAndDisactive.OnCompletedLogic += value;
        }
        remove
        {
            _listActionGmActiveAndDisactive.OnCompletedLogic -= value;
        }
    }
    public override bool IsCompletedLogic => _listActionGmActiveAndDisactive.IsCompletedLogic;

    [SerializeField] 
    private ListActionGmActiveAndDisactive _listActionGmActiveAndDisactive;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _listActionGmActiveAndDisactive.StartAction();
    }
}
