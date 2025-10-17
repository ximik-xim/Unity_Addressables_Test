using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// При запуске разблокирует(или блокирует) указ сцены
/// </summary>
public class TL_TaskSetStatusBlockScene : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    
    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = false;
    public override event Action OnCompletedLogic;
    
    [SerializeField]
    private GetPatchIntStorageBlockScene _getPatchIntStorageBlockScene;
    
    [SerializeField]
    private AbsGetStorageKeyNameScene _keyScene;

    [SerializeField]
    private bool _isBlock;
    
    private void Awake()
    {
        if (_getPatchIntStorageBlockScene.IsInit == false)
        {
            _getPatchIntStorageBlockScene.OnInit += OnInitGetDkoPatch;
        }
        
        if (_keyScene.IsInit == false)
        {
            _keyScene.OnInit += OnInitGetKeyScene;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_getPatchIntStorageBlockScene.IsInit == true)
        {
            _getPatchIntStorageBlockScene.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void OnInitGetKeyScene()
    {
        if (_keyScene.IsInit == true)
        {
            _keyScene.OnInit -= OnInitGetKeyScene;
            CheckInit();
        }
    }
    
    
    private void CheckInit()
    {
        if (_getPatchIntStorageBlockScene.IsInit == true && _keyScene.IsInit == true)
        {
            _isInit = true;
            OnInit?.Invoke();
        }
    }
    
    public override void StartLogic(DKOKeyAndTargetAction dataDKO)
    {
        _isCompletedLogic = false;

        var listSceneName = _keyScene.GetData();

        List<AbsKeyData<KeyNameScene, bool>> listData = new List<AbsKeyData<KeyNameScene, bool>>();
        foreach (var VARIABLE in listSceneName)
        {
            listData.Add(new AbsKeyData<KeyNameScene, bool>(VARIABLE, _isBlock));
        }

        _getPatchIntStorageBlockScene.GetStorageBlockScene().SetStatusBlock(listData);
        
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
}
