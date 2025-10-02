using System;
using UnityEngine;

/// <summary>
/// После завершения работы Task Loader UI
/// запускает уничтожение всех Task UI
/// (в случае логера, он тоже автоматически уничтожиться, т.к подписан на OnDestroy)
/// </summary>
public class AutoClearTaskUI : MonoBehaviour
{
    [SerializeField]
    private StorageTaskLoader _storageTaskLoader;

    [SerializeField]
    private StorageDKO_TaskLoaderUI _storageDkoTaskLoaderUI;

    [SerializeField]
    private GetDataSODataDKODataKey _keyGetDestroy;

    private void Awake()
    {
        _storageTaskLoader.OnCompleted += OnCompleted;
    }

    private void OnCompleted()
    {
        var listTask = _storageDkoTaskLoaderUI.GetAllTask();

        int maxCount = listTask.Count;
        for (int i = 0; i < maxCount; i++)
        {
            var data = (DKODataInfoT<LogicDestroyTaskUI>)listTask[i].KeyRun(_keyGetDestroy.GetData());
            LogicDestroyTaskUI logicDestroyTaskUI = data.Data;
            
            _storageDkoTaskLoaderUI.RemoveTaskUI(listTask[i]);
            
            logicDestroyTaskUI.StartDestroyObject();
            
            i--;
            maxCount--;
        }
    }

    private void OnDestroy()
    {
        _storageTaskLoader.OnCompleted -= OnCompleted;
    }

    
}
