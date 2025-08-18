using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Пример, при старте установить в указанное хранилеще все указанные такси и запустит
/// </summary>
public class ExampleStartSetTaskStorage : MonoBehaviour
{
    [SerializeField]
    private List<AbsTaskLoaderDataMono> _listTask;

    [SerializeField] 
    private StorageTaskLoader _taskStorage;

    private void Start()
    {

        foreach (var VARIABLE in _listTask)
        {
            _taskStorage.AddTaskData(VARIABLE.GetTaskInfo());
        }
        
        _taskStorage.StartLoad();
    }
}
