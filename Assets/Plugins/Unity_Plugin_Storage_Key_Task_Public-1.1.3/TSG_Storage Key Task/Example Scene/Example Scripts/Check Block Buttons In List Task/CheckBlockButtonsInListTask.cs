using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Будет реагировать на добавление Task на блокировку
/// И если есть, хотя бы 1 Task, то будет отключать кнопку
/// (Возможно надо будет вынести в плагин TSG как пример)
/// </summary>
public class CheckBlockButtonsInListTask : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _patchStorageTask;
    
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyStorageTask;
    
    private TSG_StorageKeyTaskDataMono _storageKeyTask;
    private TSG_StorageTaskDefaultData _storageTaskBlock;
    
    [SerializeField]
    private List<Button> _buttonsList = new List<Button>();
    
    private void Awake()
    {
        if (_patchStorageTask.Init == false)
        {
            _patchStorageTask.OnInit += OnInitStoragePanel;
            return;
        }

        GetDataDKO();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageTask.OnInit -= OnInitStoragePanel;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        var DKOData = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchStorageTask.GetDKO();
        _storageKeyTask = DKOData.Data;

        if (_storageKeyTask.IsInit == true)
        {
            Init();
        }
        else
        {
            _storageKeyTask.OnInit += OnInit;
        }
        
    }

    private void OnInit()
    {
        if (_storageKeyTask.IsInit == true) 
        {
            _storageKeyTask.OnInit -= OnInit;
            Init();
        }
    }

    private void Init()
    {
        if (_storageKeyTask.IsKey(_keyStorageTask.GetData()) == false)
        {
            _storageKeyTask.AddTaskData(_keyStorageTask.GetData(), new TSG_StorageTaskDefaultData());
        }
        
        _storageTaskBlock = _storageKeyTask.GetTaskData(_keyStorageTask.GetData());
        _storageTaskBlock.OnUpdateStatus += OnCheckStatusBlockButton;

        OnCheckStatusBlockButton();
    }
    
    private void OnCheckStatusBlockButton()
    {
        foreach (var VARIABLE in _buttonsList)
        {
            VARIABLE.interactable = !_storageTaskBlock.IsThereTasks();
        }
    }

    private void OnDestroy()
    {
        if (_storageTaskBlock != null) 
        {
            _storageTaskBlock.OnUpdateStatus -= OnCheckStatusBlockButton;
        }
    }
}
