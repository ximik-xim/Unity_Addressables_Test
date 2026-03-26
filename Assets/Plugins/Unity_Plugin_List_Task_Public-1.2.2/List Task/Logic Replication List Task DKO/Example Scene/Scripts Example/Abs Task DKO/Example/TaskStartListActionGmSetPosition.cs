using System;
using UnityEngine;

public class TaskStartListActionGmSetPosition : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic
    {
        add
        {
            _listActionGmSetPosition.OnCompletedLogic += value;
        }
        remove
        {
            _listActionGmSetPosition.OnCompletedLogic -= value;
        }
    }
    public override bool IsCompletedLogic => _listActionGmSetPosition.IsCompletedLogic;

    [SerializeField] 
    private ListActionGmSetPosition _listActionGmSetPosition;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _listActionGmSetPosition.StartAction();
    }
}
