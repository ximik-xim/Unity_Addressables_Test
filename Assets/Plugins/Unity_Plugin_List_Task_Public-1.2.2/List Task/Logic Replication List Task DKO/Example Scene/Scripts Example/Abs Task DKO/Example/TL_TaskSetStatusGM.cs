using System;
using System.Collections.Generic;
using UnityEngine;

public class TL_TaskSetStatusGM : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    
    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = false;
    public override event Action OnCompletedLogic;
    
    [SerializeField]
    private List<GameObject> _listGM;

    [SerializeField]
    private bool _isActive;
    
    private void Awake()
    {
        _isInit = true;
        OnInit?.Invoke();
    }
    
    public override void StartLogic(DKOKeyAndTargetAction dataDKO)
    {
        _isCompletedLogic = false;

        foreach (var VARIABLE in _listGM)
        {
            VARIABLE.SetActive(_isActive);
        }
        
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
}
