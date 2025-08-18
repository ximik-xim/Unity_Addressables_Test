using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список задач который будет запущен сразу при переходе на новую сцену
/// (у каждой сцене он свой)
/// </summary>
public class SceneStartTask : MonoBehaviour
{
    [SerializeField] 
    private bool _startTaskLoadOnInit = true;
    public bool StartTaskLoadOnInit => _startTaskLoadOnInit;
    
    [Header("Списки задач(Task) для выполнения")]
    [SerializeField] 
    private List<AbsTaskLoaderDataMono> _task;

    public event Action OnSetListTask;
    private StorageTaskLoader _listTask;
    public StorageTaskLoader ListTask => _listTask;

    public void SetListTask(StorageTaskLoader listTask)
    {
        _listTask = listTask;
        OnSetListTask?.Invoke();
    }
    
    public List<TaskLoaderData> GetTask()
    {
        List<TaskLoaderData> data = new List<TaskLoaderData>();
        foreach (var VARIABLE in _task)
        {
            data.Add(VARIABLE.GetTaskInfo());
        }
        
        return data;
    }
    
 
}
