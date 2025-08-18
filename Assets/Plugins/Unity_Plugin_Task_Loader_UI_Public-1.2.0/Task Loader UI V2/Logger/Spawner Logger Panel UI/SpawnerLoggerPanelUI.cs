using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLoggerPanelUI : MonoBehaviour
{
    [SerializeField] 
    private LoggerPanelUI _prefabUI;

    [SerializeField] 
    private LoggerData _storageLog;

    private LoggerPanelUI _example;

    public event Action OnSpawn;
    
    public void SetParent(Transform parent)
    {
        _example = Instantiate(_prefabUI, parent);

        if (_example.IsInit == true)
        {
            Init();
            return;
        }

        _example.OnInit += OnInit;
    }

    private void OnInit()
    {
        _example.OnInit -= OnInit;
        Init();
    }

    private void Init()
    {
        _example.SetData(_storageLog);
        OnSpawn?.Invoke();
    }
    
    public LoggerPanelUI GetTaskUI()
    {
        return _example;
    }
}
