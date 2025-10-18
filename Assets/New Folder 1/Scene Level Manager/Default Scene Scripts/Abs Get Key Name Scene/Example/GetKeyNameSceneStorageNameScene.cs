using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен для получения с обычного хранилеща списка имен сцен и ключей этих сцен
/// </summary>
public class GetKeyNameSceneStorageNameScene : AbsGetStorageKeyNameScene
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
    
    private List<KeyNameScene> _listKeyScene;
    
    private void Awake()
    {
        if (_isSaveList == true) 
        {
            _listKeyScene = GetListKey();
        }
        
        _isInit = true;
        OnInit?.Invoke();
    }

    private List<KeyNameScene> GetListKey()
    {
        List<KeyNameScene> list = new List<KeyNameScene>();

        foreach (var VARIABLE in _storage.GetAllData())
        {
            list.Add(new KeyNameScene(VARIABLE.GetKey()));
        }

        return list;
    }
    
    public override List<KeyNameScene> GetData()
    {
        if (_isSaveList == false)
        {
            return GetListKey();
        }
        
        return _listKeyScene;
    }
}
