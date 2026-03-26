using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSG_StorageKeyTaskDataMono : MonoBehaviour //where Data : AbsStorageTaskVisible<Key2> where Key2 : IGetKey<string>
{
    [SerializeField]
    private List<AbsKeyData<GetDataSO_TSG_KeyStorageTask, TSG_StorageTaskDefaultData>> _listInsertDataInspector = new List<AbsKeyData<GetDataSO_TSG_KeyStorageTask, TSG_StorageTaskDefaultData>>(); 
    private Dictionary<string, TSG_StorageTaskDefaultData> _taskData = new Dictionary<string, TSG_StorageTaskDefaultData>();

    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    
#if UNITY_EDITOR
    [SerializeField] 
    private bool _visibleData;

    [SerializeField]
    private List<AbsKeyData<string, TSG_StorageTaskDefaultData>> _listVisible = new List<AbsKeyData<string, TSG_StorageTaskDefaultData>>();
#endif


    private void Awake()
    {
        foreach (var VARIABLE in _listInsertDataInspector)
        {
            _taskData.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
               
#if UNITY_EDITOR
            if (_visibleData == true)
            {
                AddKeyVisible(VARIABLE.Key.GetData(), VARIABLE.Data);
            }
#endif
               
        }
   
        _isInit = true;
        OnInit?.Invoke();
    }

    public void AddTaskData(TSG_KeyStorageTask key, TSG_StorageTaskDefaultData data)
    {
        _taskData.Add(key.GetKey(), data);
        
#if UNITY_EDITOR
        if (_visibleData == true)
        {
            AddKeyVisible(key, data);
            data._visibleData = true;
        }
#endif
    }
    
    public void RemoveTaskData(TSG_KeyStorageTask key)
    {
        _taskData.Remove(key.GetKey());
        
#if UNITY_EDITOR
        if (_visibleData == true)
        {
            RemoveKeyVisible(key);
        }
#endif
    }
    
    public bool IsKey(TSG_KeyStorageTask key)
    {
        return _taskData.ContainsKey(key.GetKey());
    }

    public TSG_StorageTaskDefaultData GetTaskData(TSG_KeyStorageTask key)
    {
        return _taskData[key.GetKey()];
    }
    
#if UNITY_EDITOR
    private AbsKeyData<string, TSG_StorageTaskDefaultData> IsKeyVisible(TSG_KeyStorageTask key)
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

    private void AddKeyVisible(TSG_KeyStorageTask key, TSG_StorageTaskDefaultData dataTask)
    {
        var data = IsKeyVisible(key);
        if (data == null)
        {
            var newData = new AbsKeyData<string, TSG_StorageTaskDefaultData>(key.GetKey(), dataTask);
            
            _listVisible.Add(newData);
        }
        
        
    }

    private void RemoveKeyVisible(TSG_KeyStorageTask key)
    {
        var data = IsKeyVisible(key);
        if (data != null)
        {
            _listVisible.Remove(data);
        }
    }
#endif
    
}
