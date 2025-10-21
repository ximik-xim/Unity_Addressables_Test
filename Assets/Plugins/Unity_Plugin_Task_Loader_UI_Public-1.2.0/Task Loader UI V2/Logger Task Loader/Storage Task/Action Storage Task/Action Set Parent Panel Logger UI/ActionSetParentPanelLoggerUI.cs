using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// При добавлении Task в Storage Task
/// Получаю DKO этой Task и через него проверяю
/// есть ли у Task логика для Логера,
/// если есть, то уст родителя у созданной панели логера.
/// (а если нет, то и пофиг)
/// </summary>
public class ActionSetParentPanelLoggerUI : MonoBehaviour
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
        if (obj.ActionIsAlready(_key.GetData()) == true)
        {
            var data = (DKODataInfoT<SpawnerLoggerPanelUI>)obj.KeyRun(_key.GetData());
            data.Data.SetParent(_parent.transform);    
        }
        
    }

    private void OnDestroy()
    {
        _taskStorage.OnAddTask -= OnAddTask;
        _taskStorage.OnRemoveTask -= OnRemoveTask;
    }
}
