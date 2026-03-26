using System;
using UnityEngine;

/// <summary>
/// Получает StorageTaskLoader через SceneStartTask
/// и при завершении выполн. всех Task закрывает окно у StorageTaskLoaderUI
/// </summary>
public class StorageTaskCompletedClosePanelTaskLoader : MonoBehaviour
{
    [SerializeField] 
    private SceneStartTask _sceneStartTask;

    [SerializeField] 
    private GetDKOPatch _patchStorageTaskLoaderPanelUI;

    private StorageTaskLoaderUI _storageTaskLoaderPanelUI;
    
    private void Awake()
    {
        if (_sceneStartTask.StorageTaskLoader == null)
        {
            _sceneStartTask.OnSetStorageTaskLoader += OnInitSceneStartTask;
        }
        
        if (_patchStorageTaskLoaderPanelUI.Init == false)
        {
            _patchStorageTaskLoaderPanelUI.OnInit += OnInitPatchStorageTaskLoaderPanelUI;
        }

        CheckInit();
    }
    
    private void OnInitSceneStartTask()
    {
        if (_sceneStartTask.StorageTaskLoader != null)
        {
            _sceneStartTask.OnSetStorageTaskLoader -= OnInitSceneStartTask;
            CheckInit();
        }
    } 
    
    private void OnInitPatchStorageTaskLoaderPanelUI()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == true)
        {
            _patchStorageTaskLoaderPanelUI.OnInit -= OnInitPatchStorageTaskLoaderPanelUI;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == true && _sceneStartTask.StorageTaskLoader != null) 
        {
            _storageTaskLoaderPanelUI = _patchStorageTaskLoaderPanelUI.GetDKO<DKODataInfoT<StorageTaskLoaderUI>>().Data;
            
            _sceneStartTask.StorageTaskLoader.OnCompleted += OnCompletedTask;
        }
    }
    

    private void OnCompletedTask()
    {
        _storageTaskLoaderPanelUI.Close();
    }

    private void OnDestroy()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted -= OnCompletedTask;
    }
}
