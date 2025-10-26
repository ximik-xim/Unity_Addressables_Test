using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Обертка над обычным списком задач.
/// Тут разрешено выполнение задачи будет в зависимости от выбраного типа проверки
/// В1) Block - это если в списке Task будет хотя бы 1 Task, то значит выполнение текущего списка задач запрещено(и наоборот, если нет Task, то разрешено)
/// В2) Permission - это если в списке Task будет хотя бы 1 Task, то значит выполнение текущего списка задач разрешено(и наоборот, если нет Task, то запрещено)
/// </summary>
public class LogicListTaskDKOTypeCheckPermission : MonoBehaviour
{
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    
    [SerializeField]
    private GetDKOPatch _patchStorageTask;
    
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyStorageTask;

    private TSG_StorageKeyTaskDataMono _storageKeyTask;

    [SerializeField]
    private TypeCheckPermission _typeCheckPermission;

    [SerializeField] 
    private LogicListTaskDKO _listTaskDKO;
    
    public event Action OnCompleted
    {
        add
        {
            _listTaskDKO.OnCompleted += value;
        }
        remove
        {
            _listTaskDKO.OnCompleted -= value;
        }
    }
    public bool IsCompleted => _listTaskDKO.IsCompleted;
    
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
        
        _isInit = true;
        OnInit?.Invoke();
    }

    public bool IsBlock()
    {
        if (_typeCheckPermission == TypeCheckPermission.Block)
        {
            if (_storageKeyTask.GetTaskData(_keyStorageTask.GetData()).IsThereTasks() == true)
            {
                return true;
            }

            return false;
        }
        
        if (_typeCheckPermission == TypeCheckPermission.Permission)
        {
            if (_storageKeyTask.GetTaskData(_keyStorageTask.GetData()).IsThereTasks() == true)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public void StartTask(DKOKeyAndTargetAction dko)
    {
        if (IsBlock() == false)
        {
            _listTaskDKO.StartAction(dko);
        }
    }
    
    public TSG_StorageTaskDefaultData GetTaskData()
    {
        return _storageKeyTask.GetTaskData(_keyStorageTask.GetData());
    }
    
    
}

public enum TypeCheckPermission
{
    Block,
    Permission

}