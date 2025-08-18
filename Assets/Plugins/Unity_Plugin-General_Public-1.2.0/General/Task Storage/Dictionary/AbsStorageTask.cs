
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbsStorageTask<Key> where Key : IGetKey<string>
{
    public event Action OnUpdateStatus;

    protected Dictionary<string, TaskInfo> _task = new Dictionary<string, TaskInfo>();

    public virtual void AddTask(Key key, string text)
    {
        if (_task.Count == 0)
        {
            _task.Add(key.GetKey(), new TaskInfo(text));
            
            OnUpdateStatus?.Invoke();
            
            return;
        }

        _task.Add(key.GetKey(), new TaskInfo(text));
    }

    public virtual void RemoveTask(Key key)
    {
        if (_task.Count == 1) 
        {
            _task.Remove(key.GetKey());
            
            OnUpdateStatus?.Invoke();
            
            return;
        }
        
        _task.Remove(key.GetKey());
    }

    public virtual bool IsKeyTask(Key key)
    {
        return _task.ContainsKey(key.GetKey());
    }

    public virtual bool IsThereTasks()
    {
        if (_task.Count > 0)
        {
            return true;
        }

        return false;
    }

    public virtual void PrintAllTask(string postscript)
    {
        foreach (var VARIABLE in _task)
        {
            Debug.Log(postscript + " = | Key = " + VARIABLE.Key + " Data = " + VARIABLE.Value.GetTextTask());
        }
    }

    public List<string> GetTextAllTaskInfo()
    {
        List<string> data = new List<string>();
        foreach (var VARIABLE in _task)
        {
            string text = VARIABLE.Value.GetTextTask();
            data.Add(text);
        }

        return data;
    }
}
