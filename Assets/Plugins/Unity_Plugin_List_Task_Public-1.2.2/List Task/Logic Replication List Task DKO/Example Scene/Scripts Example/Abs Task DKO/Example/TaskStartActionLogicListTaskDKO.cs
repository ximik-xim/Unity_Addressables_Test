using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужна для запуска другого списка задач при запуске это Task
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

    /// <summary>
    /// Буду ли ждать callback от указ. списка задач
    /// </summary>
    [SerializeField]
    private bool _isWaitingTaskComplited = false;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _isCompletedLogic = false;
        
        _tileLogicListTaskTile.StartAction(tileDKO);

        if (_isWaitingTaskComplited == true)
        {
            if (_tileLogicListTaskTile.IsCompleted == false)
            {
                _tileLogicListTaskTile.OnCompleted -= OnComplited;
                _tileLogicListTaskTile.OnCompleted += OnComplited;
            }
            else
            {
                Complited();
            }
        }
        else
        {
            Complited();
        }
        
        
    }

    private void OnComplited()
    {
        if (_tileLogicListTaskTile.IsCompleted == true) 
        {
            _tileLogicListTaskTile.OnCompleted -= OnComplited;
            Complited();    
        }
    }
    
    private void Complited()
    {
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
}
