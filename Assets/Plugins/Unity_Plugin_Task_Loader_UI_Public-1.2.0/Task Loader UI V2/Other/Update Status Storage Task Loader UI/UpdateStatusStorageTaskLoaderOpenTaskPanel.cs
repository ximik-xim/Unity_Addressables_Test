using System;
using UnityEngine;

public class UpdateStatusStorageTaskLoaderOpenTaskPanel : MonoBehaviour
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
        _sceneStartTask.StorageTaskLoader.OnUpdateGeneralStatuse += OnUpdateGeneralStatuse;
    }

    private void OnUpdateGeneralStatuse(TypeStatusTaskLoad status)
    {
        if (status == TypeStatusTaskLoad.Start)
        {
            _sceneStartTask.StorageTaskLoader.OnUpdateGeneralStatuse -= OnUpdateGeneralStatuse;
            _storageTaskLoaderUI.Open();   
        }
    }
    
    private void OnDestroy()
    {
        _sceneStartTask.StorageTaskLoader.OnUpdateGeneralStatuse -= OnUpdateGeneralStatuse;
    }
    
}
