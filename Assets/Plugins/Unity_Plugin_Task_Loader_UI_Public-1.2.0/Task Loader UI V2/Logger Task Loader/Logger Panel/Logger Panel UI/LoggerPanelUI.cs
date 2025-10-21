using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Логика панели UI логера
///
/// Имеет в себе логику для переключения между типами логов
/// (тупо при наж на кнопку получаю тип лога котор. сейчас выбран,
/// обращ. в хран с параметрами текста для этого лога,
/// устанавливаю полученные параметры для текста и вывожу текст
/// котор. был помечен этим типом лога)
/// </summary>
public class LoggerPanelUI : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    private Dictionary<string, ButtonTypeLog> _typeLogButton = new Dictionary<string, ButtonTypeLog>();
    
    [SerializeField] 
    private StoragePrefabButtonTypeLog _storagePrefabButtonTypeLog;
    [SerializeField] 
    private Transform _parentButtonsTypeLog;

    private LoggerData _logStorageData;
    
    [SerializeField] 
    private GetDataSO_KeyTaskLoaderTypeLog _getKeyFullLog;
    private KeyTaskLoaderTypeLog _currentKeyType;

    [SerializeField] 
    private GameObject _taskLoggerPanel;
    
    [SerializeField] 
    private Text _textLogs;

    //По идеи это можно отделить на отдельны модуль(на будущее)
    [SerializeField] 
    private StoragePrefabParameterTextTypeLog _storageParameterText;
    
    [SerializeField] 
    private GameObject _destroyObject;
    
    private void Awake()
    {
        if (_storageParameterText.IsInit == true && _storagePrefabButtonTypeLog.IsInit == true)
        {
            Init();
        }
        else
        {
            if (_storageParameterText.IsInit == false)
            {
                _storageParameterText.OnInit += CheckInitStorageParameterText;
            }

            if (_storagePrefabButtonTypeLog.IsInit == false)
            {
                _storagePrefabButtonTypeLog.OnInit += CheckInitStoragePrefabButtonTypeLog;
            }
            
        }
    }
    
    private void CheckInitStorageParameterText()
    {
        _storageParameterText.OnInit -= CheckInitStorageParameterText;
        Check();
    }

    private void CheckInitStoragePrefabButtonTypeLog()
    {
        _storagePrefabButtonTypeLog.OnInit -= CheckInitStoragePrefabButtonTypeLog;
        Check();
    }

    private void Check()
    {
        if (_storageParameterText.IsInit == true && _storagePrefabButtonTypeLog.IsInit == true)
        {
            Init();
        }
    }

    private void Init()
    {
        SpawnButtonTypeLog(_getKeyFullLog.GetData());
        
        _isInit = true;
        OnInit?.Invoke();
    }

    public void SetData(LoggerData logData)
    {
        if (_logStorageData != null) 
        {
            _logStorageData.OnAddLogData -= OnAddLogData;
            _logStorageData.OnClearData -= OnClearData;
        }
        
        _logStorageData = logData;
        
        _currentKeyType = _getKeyFullLog.GetData();
        
        foreach (var VARIABLE in _logStorageData.GetListLog())
        {
            SpawnButtonTypeLog(VARIABLE.Key);
        }

        _logStorageData.OnAddLogData += OnAddLogData;
        _logStorageData.OnClearData += OnClearData;
        
        OnButtonClickTypeLog(_currentKeyType);
    }

   

    private void OnClearData()
    {
        foreach (var VARIABLE in _typeLogButton.Keys)
        {
            if (VARIABLE != _getKeyFullLog.GetData().GetKey()) 
            {
                _typeLogButton[VARIABLE].Close();    
            }
            
        }
        
        _currentKeyType = _getKeyFullLog.GetData();
        
        _textLogs.text = "";
    }

    private void OnAddLogData(AbsKeyData<KeyTaskLoaderTypeLog, string> data)
    {
        SpawnButtonTypeLog(data.Key);
        
        if (_currentKeyType == _getKeyFullLog.GetData())
        {
            _textLogs.text += data.Data + "\n";
        }
        else
        {
            if (data.Key.GetKey() == _currentKeyType.GetKey())
            {
                _textLogs.text += data.Data + "\n";    
            }
        }
    }

    private void SpawnButtonTypeLog(KeyTaskLoaderTypeLog keyTypeLog)
    {
        if (_typeLogButton.ContainsKey(keyTypeLog.GetKey()) == false)
        {
            var prefab = _storagePrefabButtonTypeLog.GetPrefabButton(keyTypeLog);
            var example = Instantiate(prefab, _parentButtonsTypeLog);
            
            example.Open();

            example.OnButtonClick += OnButtonClickTypeLog;
            
            _typeLogButton.Add(keyTypeLog.GetKey(), example);
        }
        else
        {
            _typeLogButton[keyTypeLog.GetKey()].Open();
        }
    }
    
    

    private void OnButtonClickTypeLog(KeyTaskLoaderTypeLog key)
    {
        _currentKeyType = key;
        
        var parameterText= _storageParameterText.GetParameterText(_currentKeyType);
        _textLogs.color = parameterText.GetColorText();
        
        PrintText();
    }

    private void PrintText()
    {
        _textLogs.text = "";
        
        if (_currentKeyType == _getKeyFullLog.GetData()) 
        {
            foreach (var VARIABLE in _logStorageData.GetListLog())
            {
                _textLogs.text += VARIABLE.Data + "\n";
            }
        }
        else
        {
            foreach (var VARIABLE in _logStorageData.GetListLog())
            {
                if (VARIABLE.Key.GetKey() == _currentKeyType.GetKey())
                {
                    _textLogs.text += VARIABLE.Data + "\n";    
                }
            }
        }
        
    }

    
    
    public void Open()
    {
        _taskLoggerPanel.SetActive(true);
    }
    
    public void Close()
    {
        _taskLoggerPanel.SetActive(false);
    }

    public void StartDestroy()
    {
        Destroy(_destroyObject);
    }
    
    private void OnDestroy()
    {
        if (_logStorageData != null)
        {
            _logStorageData.OnAddLogData -= OnAddLogData;
            _logStorageData.OnClearData -= OnClearData;
        }
    }
    
    
}
