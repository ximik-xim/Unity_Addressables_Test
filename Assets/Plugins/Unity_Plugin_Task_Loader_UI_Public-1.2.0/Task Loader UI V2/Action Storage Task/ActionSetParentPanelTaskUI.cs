using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Тупо устанавливает родителя для панели UI у Task
/// </summary>
public class ActionSetParentPanelTaskUI : MonoBehaviour
{
    [SerializeField] 
    private StorageTaskLoader _taskStorage;

    [SerializeField] 
    private GameObject _parent;

    [SerializeField] 
    private GetDataSODataDKODataKey _key;
    
    private void Awake()
    {
        _taskStorage.OnAddTask += OnAddTask;
        _taskStorage.OnRemoveTask += OnRemoveTask;
    }

    private void OnRemoveTask(DKOKeyAndTargetAction obj)
    {

    }

    private void OnAddTask(DKOKeyAndTargetAction obj)
    {
        var data = (DKODataInfoT<SpawnerTaskPanelUI>)obj.KeyRun(_key.GetData());
        data.Data.SetParent(_parent.transform);
    }

    private void OnDestroy()
    {
        _taskStorage.OnAddTask -= OnAddTask;
        _taskStorage.OnRemoveTask -= OnRemoveTask;
    }
}
