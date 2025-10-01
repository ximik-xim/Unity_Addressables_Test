using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// По окончанию работы Task Loader UI запустит загрузку сцены
/// </summary>
public class SceneStartTaskComplitedStartLoadScene : MonoBehaviour
{
    [SerializeField]
    private SceneStartTask _sceneStartTask;
    
    [SerializeField] 
    private AbsLoadTargetSceneKey _loadSceneAddressables;
    
    private void Awake()
    {
        if (_loadSceneAddressables.IsInit == false)
        {
            _loadSceneAddressables.OnInit += OnInitSceneLoad;
        }
    }
    
    private void OnInitSceneLoad()
    {
        if (_loadSceneAddressables.IsInit == true)
        {
            _loadSceneAddressables.OnInit -= OnInitSceneLoad;
            CheckInit();
        }
        
    }
    
    private void CheckInit()
    {
        if (_loadSceneAddressables.IsInit == true)  
        {
            Init();
        }
    }
    
    
    private void Init()
    {
        if (_sceneStartTask.StorageTaskLoader == null)
        {
            _sceneStartTask.OnSetStorageTaskLoader += OnSetListTask;
        }
        else
        {
            SetListTask();
        }
    }

    private void OnSetListTask()
    {
        if (_sceneStartTask.StorageTaskLoader != null)
        {
            _sceneStartTask.OnSetStorageTaskLoader -= OnSetListTask;

            SetListTask();
        }
    }

    private void SetListTask()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted += OnCompletedTaskLoad;
    }

    private void OnCompletedTaskLoad()
    {
        _sceneStartTask.StorageTaskLoader.OnCompleted -= OnCompletedTaskLoad;
        
        _loadSceneAddressables.StartLoadScene();
    }
}
