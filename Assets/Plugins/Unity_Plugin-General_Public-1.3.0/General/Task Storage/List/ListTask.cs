using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListTask
{
    public event Action OnUpdateStatus;
    
    [SerializeField]
    protected List<TaskInfo> _task = new List<TaskInfo>();

    public virtual void AddTask(TaskInfo taskInfo)
    {
        if (_task.Count == 0)
        {
            _task.Add(taskInfo);
            
            OnUpdateStatus?.Invoke();
            
            return;
        }

        _task.Add(taskInfo);
    }

    public virtual void RemoveTask(TaskInfo taskInfo)
    {
        if (IsInsertTask(taskInfo) == true)
        {
            if (_task.Count == 1) 
            {
                _task.Remove(taskInfo);
            
                OnUpdateStatus?.Invoke();
            
                return;
            }
        
            _task.Remove(taskInfo);
        }
    }

    public virtual void RemoveAllTask()
    {
        if (_task.Count > 0)
        {
            _task.Clear();
            OnUpdateStatus?.Invoke();
        }
    }

    public virtual bool IsInsertTask(string text)
    {

        foreach (var VARIABLE in _task)
        {
            if (VARIABLE.GetTextTask() == text)
            {
                return true;
            }
        }

        return false;
    }
    
    public virtual bool IsInsertTask(TaskInfo taskInfo)
    {
        return _task.Contains(taskInfo);
    }

    public virtual bool IsThereTasks()
    {
        if (_task.Count > 0)
        {
            return true;
        }

        return false;
    }

    public List<TaskInfo> GetAllTask()
    {
        List<TaskInfo> list = new List<TaskInfo>();
        foreach (var VARIABLE in _task)
        {
            list.Add(VARIABLE); 
        }

        return list;
    }

    public virtual void PrintAllTask(string postscript)
    {
        foreach (var VARIABLE in _task)
        {
            Debug.Log(" Data = " + VARIABLE.GetTextTask());
        }
    }
}
