using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// при запуске добавит все указ. Task в StorageTaskLoader
/// и запустит выполн. задач
/// 
/// (имеет логику отслеж. когда можно добавить и запустить выполнение Task)
/// </summary>
public class StartSetListTaskInStorageTaskLoader : MonoBehaviour
{
    [SerializeField]
    private List<AbsTaskLoaderDataMono> _listTask;

    [SerializeField] 
    private StorageTaskLoader _taskStorage;
    
    private void Awake()
    {
        if (_taskStorage.IsInit == false)
        {
            _taskStorage.OnInit += OnInitTaskStorage;
        }
        else
        {
            InitTaskStorage();
        }
    }

    private void OnInitTaskStorage()
    {
        if (_taskStorage.IsInit == true) 
        {
            _taskStorage.OnInit -= OnInitTaskStorage;
            InitTaskStorage();
        }
    }
    
    private void InitTaskStorage()
    {
        CheckComplitedTask();
    }


    private void CheckComplitedTask()
    {
        if (_taskStorage.IsSceneLoadTask == false && _taskStorage.IsStartLoadTask == false) 
        {
            Debug.Log("Добавление следующих Task на выполнение");
            
            _taskStorage.OnSceneLoadTask -= OnCheckComplitedTask;
            _taskStorage.OnCompleted -= OnCheckComplitedTask;
            
            foreach (var VARIABLE in _listTask)
            {
                _taskStorage.AddTaskData(VARIABLE.GetTaskInfo());
            }
        
            _taskStorage.StartLoad();
        }
        else
        {
            _taskStorage.OnSceneLoadTask -= OnCheckComplitedTask;
            _taskStorage.OnSceneLoadTask += OnCheckComplitedTask;
            
            _taskStorage.OnCompleted -= OnCheckComplitedTask;
            _taskStorage.OnCompleted += OnCheckComplitedTask;
        }
    }

    private void OnCheckComplitedTask()
    {
        CheckComplitedTask();
    }

    private void OnCheckComplitedTask(bool obj)
    {
        CheckComplitedTask();
    }

    private void OnDestroy()
    {
        _taskStorage.OnSceneLoadTask -= OnCheckComplitedTask;
        _taskStorage.OnCompleted -= CheckComplitedTask;
    }
}
