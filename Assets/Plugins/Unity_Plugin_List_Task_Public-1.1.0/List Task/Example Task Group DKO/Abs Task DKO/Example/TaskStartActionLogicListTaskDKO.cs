using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужна для запуска другой задачи из текущей
/// НЕ ЖДЕТ ПОКА ПРИЙДЕТ CALL BACK об завершении Task
/// </summary>
public class TaskStartActionLogicListTaskDKO : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic;
    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = true;
    
    [SerializeField] 
    private LogicListTaskDKO _tileLogicListTaskTile;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _isCompletedLogic = false;
        
        _tileLogicListTaskTile.StartAction(tileDKO);

        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
}
