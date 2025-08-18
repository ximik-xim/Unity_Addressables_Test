using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleUserStorage : MonoBehaviour
{
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;

    [SerializeField]
    private List<AbsKeyData<GetDataSO_TList_Example, ExampleUserInfo>> _list = new List<AbsKeyData<GetDataSO_TList_Example, ExampleUserInfo>>();

    private Dictionary<string, ExampleUserInfo> _dictionary = new Dictionary<string, ExampleUserInfo>();

 #if UNITY_EDITOR
    [SerializeField] 
    private bool _visibleData;

    [SerializeField]
    private List<AbsKeyData<string, ExampleUserInfo>> _listVisible = new List<AbsKeyData<string, ExampleUserInfo>>();
 #endif
    
    private void Awake()
    {
        foreach (var VARIABLE in _list)
        {
            _dictionary.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
            
#if UNITY_EDITOR
            if (_visibleData == true)
            {
                AddKeyVisible(VARIABLE.Key.GetData(),VARIABLE.Data);
            }
#endif
            
        }

        _init = true;
        OnInit?.Invoke();
    }
    
    public bool DataIsInsert(TList_ExampleKey key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }
    
    public ExampleUserInfo GetUserInfo(TList_ExampleKey key)
    {
        return _dictionary[key.GetKey()];
    }

    public void AddUserInfo(TList_ExampleKey key, ExampleUserInfo userInfo)
    {
        _dictionary.Add(key.GetKey(), userInfo);
        
#if UNITY_EDITOR
        if (_visibleData == true)
        {
            AddKeyVisible(key,userInfo);
        }
#endif
    }

    public void RemoveUserInfo(TList_ExampleKey key)
    {
        _dictionary.Remove(key.GetKey());
        
#if UNITY_EDITOR
        if (_visibleData == true)
        {
            RemoveKeyVisible(key);
        }
#endif
    }

#if UNITY_EDITOR
    private AbsKeyData<string, ExampleUserInfo> IsKeyVisible(TList_ExampleKey key)
    {
        foreach (var VARIABLE in _listVisible)
        {
            if (VARIABLE.Key == key.GetKey())
            {
                return VARIABLE;
            }     
        }

        return null;
    }

    private void AddKeyVisible(TList_ExampleKey key, ExampleUserInfo userInfo)
    {
        var data = IsKeyVisible(key);
        if (data == null)
        {
            var newData = new AbsKeyData<string, ExampleUserInfo>(key.GetKey(), userInfo);
            
            _listVisible.Add(newData);
        }
        
        
    }

    private void RemoveKeyVisible(TList_ExampleKey key)
    {
        var data = IsKeyVisible(key);
        if (data != null)
        {
            _listVisible.Remove(data);
        }
    }
#endif
    
}
