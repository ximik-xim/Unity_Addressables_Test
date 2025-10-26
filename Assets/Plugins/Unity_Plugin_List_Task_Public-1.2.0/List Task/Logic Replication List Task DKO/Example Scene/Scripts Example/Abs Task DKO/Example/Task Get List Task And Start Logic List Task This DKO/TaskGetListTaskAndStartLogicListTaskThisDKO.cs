using System;
using UnityEngine;

/// <summary>
/// Через перед. в аргументы DKO, получит список задча(по указ. ключу) и запустит его
/// </summary>
public class TaskGetListTaskAndStartLogicListTaskThisDKO : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    public override event Action OnCompletedLogic;
    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = true;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _keyGetListAction;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void StartLogic(DKOKeyAndTargetAction tileDKO)
    {
        _isCompletedLogic = false;

        var listAction = (DKODataInfoT<LogicListTaskDKO>)tileDKO.KeyRun(_keyGetListAction.GetData());
        listAction.Data.StartAction(tileDKO);
        
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
}
