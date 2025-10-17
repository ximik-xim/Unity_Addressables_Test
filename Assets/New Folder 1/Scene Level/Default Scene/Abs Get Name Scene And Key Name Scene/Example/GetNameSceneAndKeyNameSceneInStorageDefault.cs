using System;
using System.Collections.Generic;
using UnityEngine;

public class GetNameSceneAndKeyNameSceneInStorageDefault : AbsGetNameSceneAndKeyNameScene
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
    
    [SerializeField] 
    private SO_Data_NameSceneAndKeyString _storage;
    
    /// <summary>
    /// сохранять ли список ключей(нужно что бы не плодить ненужные ключи по 100 раз) 
    /// </summary>
    [SerializeField]
    private bool _isSaveList = true;
    
    private List<AbsKeyData<string, KeyNameScene>> _listKeyScene;
    
    private void Awake()
    {
        if (_isSaveList == true) 
        {
            _listKeyScene = GetListKey();
        }
        
        _isInit = true;
        OnInit?.Invoke();
    }

    private List<AbsKeyData<string, KeyNameScene>> GetListKey()
    {
        List<AbsKeyData<string, KeyNameScene>> list = new List<AbsKeyData<string, KeyNameScene>>();

        foreach (var VARIABLE in _storage.GetAllData())
        {
            list.Add(new AbsKeyData<string, KeyNameScene>(VARIABLE.GetNameScene(), new KeyNameScene(VARIABLE.GetKey())));
        }

        return list;
    }
    
    public override List<AbsKeyData<string, KeyNameScene>> GetData()
    {
        if (_isSaveList == false)
        {
            return GetListKey();
        }
        
        return _listKeyScene;
    }
}
