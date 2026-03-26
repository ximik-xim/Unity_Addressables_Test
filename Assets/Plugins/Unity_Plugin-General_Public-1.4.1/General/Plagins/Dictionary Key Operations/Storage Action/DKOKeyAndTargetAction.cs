using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Хранилеще Task по ключу
/// </summary>
public class DKOKeyAndTargetAction : MonoBehaviour
{
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    public event Action<string> OnInitElement;
    
    [SerializeField]
    private List<AbsKeyData<GetDataSODataDKODataKey, DKOTargetAction>> _list = new List<AbsKeyData<GetDataSODataDKODataKey, DKOTargetAction>>();

    private Dictionary<string, DKOTargetAction> _dictionary = new Dictionary<string, DKOTargetAction>();
    
    private void Awake()
    {
        foreach (var VARIABLE in _list)
        {
            _dictionary.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
        }

        StartInit();
    }

    private void StartInit()
    {
        List<AbsKeyData<GetDataSODataDKODataKey, DKOTargetAction>> _buffer = new List<AbsKeyData<GetDataSODataDKODataKey, DKOTargetAction>>();
        bool _isStart = false;
        
        StartLogic();

        void StartLogic()
        {
            if (_isInit == false)
            {
                _isStart = true;

                foreach (var VARIABLE in _list)
                {
                    if (VARIABLE.Data.IsInit == false)
                    {
                        _buffer.Add(VARIABLE);
                        VARIABLE.Data.OnInit += CheckInitCompleted;
                    }
                    else
                    {
                        OnInitElement?.Invoke(VARIABLE.Key.GetData().GetKey());
                    }
                    
                }

                _isStart = false;

                CheckInitCompleted();
            }
        }

        void CheckInitCompleted()
        {
            if (_isStart == false) 
            {
                int targetCount = _buffer.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (_buffer[i].Data.IsInit == true)
                    {
                        _buffer[i].Data.OnInit -= CheckInitCompleted;
                        OnInitElement?.Invoke(_buffer[i].Key.GetData().GetKey());
                        _buffer.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (_buffer.Count == 0)
                {
                    InitCompleted();
                }
            }
        }
    }

    private void InitCompleted()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public int GetCountAction()
    {
        return _dictionary.Count;
    }

    public bool EnableKeyInitData(DKODataKey keyAction)
    {
        foreach (var VARIABLE in _list)
        {
            if (VARIABLE.Key.GetData().GetKey() == keyAction.GetKey())
            {
                return true;
            }
        }

        return false;
    }

    public bool ActionIsAlready(DKODataKey key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }

    public DKODataRund KeyRun(DKODataKey keyAction, DKODataRund dkoDataRund = null)
    { 
        return _dictionary[keyAction.GetKey()].Run(dkoDataRund);
    }

    public T KeyRun<T>(DKODataKey keyAction, DKODataRund dkoDataRund = null) where T : DKODataRund
    {
        DKODataRund data = _dictionary[keyAction.GetKey()].Run(dkoDataRund);

        T returnData = null;

        if (data is T == true)
        {
            returnData = (T)data;
        }
        
        return returnData;
    }
}

[System.Serializable]
public class DKODataKey
{
    [SerializeField]
    private string key;

    public string GetKey()
    {
        return key;
    }
}

[System.Serializable]
public class DKODataRund
{
    
}