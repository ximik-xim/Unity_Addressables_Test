using System;
using UnityEngine;

public class TaskStartListActionButtonActiveAndDisactive : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic
    {
        add
        {
            _listActionButtonActiveAndDisactive.OnCompletedLogic += value;
        }
        remove
        {
            _listActionButtonActiveAndDisactive.OnCompletedLogic -= value;
        }
    }
    public override bool IsCompletedLogic => _listActionButtonActiveAndDisactive.IsCompletedLogic;

    [SerializeField] 
    private ListActionButtonActiveAndDisactive _listActionButtonActiveAndDisactive;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _listActionButtonActiveAndDisactive.StartAction();
    }
}
