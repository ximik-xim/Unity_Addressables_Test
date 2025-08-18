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
        if (_sceneStartTask.ListTask == null)
        {
            _sceneStartTask.OnSetListTask += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _sceneStartTask.OnSetListTask -= OnInit;
        Init();
    }

    private void Init()
    {
        _sceneStartTask.ListTask.OnUpdateGeneralStatuse += OnUpdateGeneralStatuse;
    }

    private void OnUpdateGeneralStatuse(TypeStatusTaskLoad status)
    {
        if (status == TypeStatusTaskLoad.Start)
        {
            _sceneStartTask.ListTask.OnUpdateGeneralStatuse -= OnUpdateGeneralStatuse;
            _storageTaskLoaderUI.Open();   
        }
    }
    
    private void OnDestroy()
    {
        _sceneStartTask.ListTask.OnUpdateGeneralStatuse -= OnUpdateGeneralStatuse;
    }
    
}
