using System;
using UnityEngine;

public class TaskStartListActionGmSetHeightAndWidth : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic
    {
        add
        {
            _listActionGmSetHeightAndWidth.OnCompletedLogic += value;
        }
        remove
        {
            _listActionGmSetHeightAndWidth.OnCompletedLogic -= value;
        }
    }
    public override bool IsCompletedLogic => _listActionGmSetHeightAndWidth.IsCompletedLogic;

    [SerializeField] 
    private ListActionGmSetHeightAndWidth _listActionGmSetHeightAndWidth;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _listActionGmSetHeightAndWidth.StartAction();
    }
}
