using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerData : MonoBehaviour
{
    /// <summary>
    /// Будут ли логи дублироваться в консоль
    /// </summary>
    [SerializeField]
    private bool _duplicateLogConsole = true;
    
    private List<AbsKeyData<KeyTaskLoaderTypeLog, string>> _logs = new List<AbsKeyData<KeyTaskLoaderTypeLog, string>>();

    public event Action<AbsKeyData<KeyTaskLoaderTypeLog, string>> OnAddLogData;
    public event Action OnClearData;
    
    public event Action OnUpdateData;
    
    public void DebugLog(KeyTaskLoaderTypeLog keyLog, string text)
    {
        if (_duplicateLogConsole == true)
        {
            Debug.Log("-> " + text);
        }
        
        var logData = new AbsKeyData<KeyTaskLoaderTypeLog, string>(keyLog, text);
        _logs.Add(logData);
        OnAddLogData?.Invoke(logData);
        OnUpdateData?.Invoke();
    }

    public void ClearAllDataLog()
    {
        _logs.Clear();
        OnClearData?.Invoke();
        OnUpdateData?.Invoke();
    }

    public List<AbsKeyData<KeyTaskLoaderTypeLog, string>> GetListLog()
    {
        List<AbsKeyData<KeyTaskLoaderTypeLog, string>> data = new List<AbsKeyData<KeyTaskLoaderTypeLog, string>>();
        
        foreach (var VARIABLE in _logs)
        {
            data.Add(VARIABLE);
        }

        return data;
    }
    
}
