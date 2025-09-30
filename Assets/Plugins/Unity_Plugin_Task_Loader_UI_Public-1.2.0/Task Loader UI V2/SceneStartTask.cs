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
    
    /// <summary>
    /// Иници. ли хран.
    /// (оно иниц. когда все Task буду готовы к запуску, а значит к передаче в Task Loader)
    /// </summary>
    public event Action OnInit;
    private bool _isInit = false;
    public bool IsInit => _isInit;
    
    [Header("Списки задач(Task) для выполнения")]
    [SerializeField] 
    private List<AbsTaskLoaderDataMono> _task;

    public event Action OnSetListTask;
    private StorageTaskLoader _listTask;
    public StorageTaskLoader ListTask => _listTask;
    
    private void Awake()
    {
        List<AbsTaskLoaderDataMono> _buffer = new List<AbsTaskLoaderDataMono>();
        bool _isStart = false;

        StartLogic();

        void StartLogic()
        {
            _isStart = true;

            foreach (var VARIABLE in _task)
            {
                if (VARIABLE.IsInit == false)
                {
                    _buffer.Add(VARIABLE);
                    VARIABLE.OnInit += CheckInit;
                }
            }

            _isStart = false;

            CheckInit();
        }

        void CheckInit()
        {
            if (_isStart == false)
            {
                int targetCount = _buffer.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (_buffer[i].IsInit == true)
                    {
                        _buffer[i].OnInit -= CheckInit;
                        _buffer.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (_buffer.Count == 0)
                {
                    Completed();
                }
            }
        }
    }

    private void Completed()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

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
