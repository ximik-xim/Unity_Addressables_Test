using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// При добавлении Task в список задач добавляет абстракцию для включения и выключения обьекта в словарь
/// (ключем являеться DKO)
/// </summary>
public class ActionAddAbsClosOpenPanelTask : MonoBehaviour
{
    [SerializeField] 
    private StorageTaskLoader _taskStorage;

    [SerializeField] 
    private StorageListAbsClosOpenPanelTask _storage;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _key;
    
    private void Start()
    {
        _taskStorage.OnAddTask += OnAddTask;
        _taskStorage.OnRemoveTask += OnRemoveTask;
    }

    private void OnRemoveTask(DKOKeyAndTargetAction obj)
    {
        var data = (DKODataInfoT<CloseOpenPanelTaskUI>)obj.KeyRun(_key.GetData());
        _storage.RemoveData(obj);
        data.Data.ClosePanel();
    }

    private void OnAddTask(DKOKeyAndTargetAction obj)
    {
        var data = (DKODataInfoT<CloseOpenPanelTaskUI>)obj.KeyRun(_key.GetData());
        _storage.AddData(obj,data.Data);
        data.Data.OpenPanel();
    }


    private void OnDestroy()
    {
        _taskStorage.OnAddTask -= OnAddTask;
        _taskStorage.OnRemoveTask -= OnRemoveTask;
    }
    
}
