using System;
using UnityEngine;

/// <summary>
/// Нужен что бы через DKO полуить StorageTaskLoader
/// затем добавить в него указ Task и запустить выполнения задачи в StorageTaskLoader
/// </summary>
public class AddTaskInStorageTaskLoader : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private GetDKOPatch _patchTaskLoaderUI;

    private StorageTaskLoader _taskLoader;

    [SerializeField]
    private AbsTaskLoaderDataMono _taskData;
    
    private void Awake()
    {
        if (_patchTaskLoaderUI.Init == false)
        {
            _patchTaskLoaderUI.OnInit += OnInitGetDkoPatch;
        }
        
        if (_taskData.IsInit == false)
        {
            _taskData.OnInit += OnInitTask;
        }
        
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_patchTaskLoaderUI.Init == true)
        {
            _patchTaskLoaderUI.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void OnInitTask()
    {
        if (_taskData.IsInit == true)
        {
            _taskData.OnInit -= OnInitTask;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_patchTaskLoaderUI.Init == true && _taskData.IsInit == true)  
        {
            InitData();
        }
    }

    private void InitData()
    {
        _taskLoader = _patchTaskLoaderUI.GetDKO<DKODataInfoT<StorageTaskLoader>>().Data;

        _isInit = true;
        OnInit?.Invoke();
    }
    
    public void StartLoadScene()
    {
        _taskLoader.AddTaskData(_taskData.GetTaskInfo());
        _taskLoader.StartLoad();
    }
}
