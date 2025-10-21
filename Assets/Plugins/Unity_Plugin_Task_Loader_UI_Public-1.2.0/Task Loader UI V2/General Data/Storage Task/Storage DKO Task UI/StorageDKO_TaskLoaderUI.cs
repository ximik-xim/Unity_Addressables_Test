using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранилеще с UI Task, нужно что бы ими можно было упровлять из вне
/// (примеру отключать или включать, или удалять, и т.д)
/// </summary>
public class StorageDKO_TaskLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private StorageTaskLoader _taskStorage;

    [SerializeField]
    private List<DKOKeyAndTargetAction> _listTaskUI;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _key;
    
    private void Awake()
    {
        _taskStorage.OnAddTask += OnAddTask;
    }

    private void OnAddTask(DKOKeyAndTargetAction obj)
    {
        var data = (DKODataInfoT<SpawnerTaskPanelUI>)obj.KeyRun(_key.GetData());
        SpawnerTaskPanelUI spawnerTaskPanelUI = data.Data;
       
        if (spawnerTaskPanelUI.IsInit == true)
        {
            InitTaskUI();
        }
        else
        {
            spawnerTaskPanelUI.OnInit += OnInitTaskUI;
        }
        
        void OnInitTaskUI()
        {
            if (data.Data.IsInit == true)
            {
                data.Data.OnInit += OnInitTaskUI;
                InitTaskUI();
            }
        }
        
        void InitTaskUI()
        {
            _listTaskUI.Add(data.Data.GetTaskUI().GetDKO());
        }
    }

    public IReadOnlyList<DKOKeyAndTargetAction> GetAllTask()
    {
        return _listTaskUI;
    }

    public void RemoveTaskUI(DKOKeyAndTargetAction obj)
    {
        for (int i = 0; i < _listTaskUI.Count; i++)
        {
            if (_listTaskUI[i] == obj) 
            {
                _listTaskUI.RemoveAt(i);
                break;
            }    
        }
    }

    private void OnDestroy()
    {
        _taskStorage.OnAddTask -= OnAddTask;
    }
}
