using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

/// <summary>
/// Хранилеще статусов сцен (заблокированы или разблокированы)
/// </summary>
public class StorageBlockScene : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private AbsLogicLoadInStorageBlockScene _absLogicLoad;
    
    [SerializeField]
    private AbsLogicSaveInStorageBlockScene _absLogicSave; 

    /// <summary>
    /// Если нету в словаре статуса сцены(заблокир она или нет)
    /// То возращаю по эмолчаю это bool знач
    /// (true - знач по умолчанию заблокир. (все что не разрешено = запрещено))
    /// (false - знач по умолчанию разблокир. (все что не запрещено = разрешено)) 
    /// </summary>
    private bool _defaultIsBlock = true;
        
    private Dictionary<string, bool> _blockList = new Dictionary<string, bool>();

#if  UNITY_EDITOR
    [SerializeField]
    private List<AbsKeyData<string, bool>> _visibleData = new List<AbsKeyData<string, bool>>();
#endif
    
    public event Action OnUpdateData;

    private void Awake()
    {
        if (_absLogicLoad.IsInit == false)
        {
            _absLogicLoad.OnInit += OnInitLogicLoad;
        }
        
        if (_absLogicSave.IsInit == false)
        {
            _absLogicSave.OnInit += OnInitLogicSave;
        }

        CheckInit();
    }

    private void OnInitLogicLoad()
    {
        if (_absLogicLoad.IsInit == true)
        {
            _absLogicLoad.OnInit -= OnInitLogicLoad;
            CheckInit();
        }
    }
    
    private void OnInitLogicSave()
    {
        if (_absLogicSave.IsInit == true)
        {
            _absLogicSave.OnInit -= OnInitLogicSave;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_absLogicLoad.IsInit == true && _absLogicSave.IsInit == true) 
        {
            Init();
        }
    }
    
    private void Init()
    {
        LoadData();

        _isInit = true;
        OnInit?.Invoke();
        
        OnUpdateData?.Invoke();
    }

    public void SetStatusBlock(KeyNameScene keyNameScene, bool status)
    {
        if (_blockList.ContainsKey(keyNameScene.GetKey()) == false)
        {
            _blockList.Add(keyNameScene.GetKey(), status);
#if UNITY_EDITOR
            _visibleData.Add(new AbsKeyData<string, bool>(keyNameScene.GetKey(), status));
#endif
        }
        else
        {
#if UNITY_EDITOR
            foreach (var VARIABLE in _visibleData)
            {
                if (VARIABLE.Key == keyNameScene.GetKey())
                {
                    VARIABLE.Data = status;
                }
            }
#endif
            _blockList[keyNameScene.GetKey()] = status;
        }

        SaveData();

        OnUpdateData?.Invoke();
    }

    public bool GetIsBlock(KeyNameScene keyNameScene)
    {
        if (_blockList.ContainsKey(keyNameScene.GetKey()) == true)
        {
            return _blockList[keyNameScene.GetKey()];
        }
        
        if (_defaultIsBlock == true)
        {
            return true;
        }

        return false;
    }

    public bool IsContainsKey(KeyNameScene keyNameScene)
    {
        return _blockList.ContainsKey(keyNameScene.GetKey());
    }

    private void LoadData()
    {
        _blockList.Clear();
        
        JsDataStorageBlockScene dataJS = _absLogicLoad.GetSaveDataJS();
        foreach (var VARIABLE in dataJS.ListData)
        {
            _blockList.Add(VARIABLE.Key, VARIABLE.Data);
            
#if  UNITY_EDITOR
            _visibleData.Add(new AbsKeyData<string, bool>(VARIABLE.Key, VARIABLE.Data));
#endif
        }
    }
    
    private void SaveData()
    {
        List<AbsKeyData<string, bool>> data = new List<AbsKeyData<string, bool>>();
        
        foreach (var VARIABLE in _blockList.Keys)
        {
            data.Add(new AbsKeyData<string, bool>(VARIABLE, _blockList[VARIABLE]));
        }

        var dataJs = new JsDataStorageBlockScene(data);
        
        _absLogicSave.SetSaveDataJS(dataJs);
        _absLogicSave.SaveDataJS();
    }
    
}
