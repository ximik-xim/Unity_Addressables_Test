using System;
using UnityEngine;

public class StorageTaskCompletedClosePanelTaskLoader : MonoBehaviour
{
    [SerializeField] 
    private SceneStartTask _sceneStartTask;

    [SerializeField] 
    private StorageTaskLoaderUI _storageTaskLoaderUI;

    private void Awake()
    {
        if (_sceneStartTask.StorageTaskLoader == null)
        {
            _sceneStartTask.OnSetStorageTaskLoader += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _sceneStartTask.OnSetStorageTaskLoader -= OnInit;
        Init();
    }

    private void Init()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted += OnCompletedTask;
    }

    private void OnCompletedTask()
    {
        _storageTaskLoaderUI.Close();
    }

    private void OnDestroy()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted -= OnCompletedTask;
    }
}
