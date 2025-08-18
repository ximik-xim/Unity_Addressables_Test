using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskLoggerLoaderDataMonoExample : AbsTaskLoggerLoaderDataMono
{
    [SerializeField] 
    private float _timeStopTask = 2;
    
    private void Awake()
    {
        InitTask();
    }

    protected override void StartLogic()
    {
        UpdateStatus(TypeStatusTaskLoad.Start);
        UpdateStatus(TypeStatusTaskLoad.Load);
        
        StartCoroutine(_enumerator());
        _storageLog.DebugLog(_storageTypeLog.GetKeyWarningLog(), "- Начало выполнение задачи");
    }

    protected override void BreakTask()
    {
   
    }

    protected override void ResetStatusTask()
    {
        
    }
    
    
    
    IEnumerator _enumerator()
    {
        yield return new WaitForSeconds(_timeStopTask);
        UpdatePercentage(GetTaskInfo().PercentComplited + 10f);  
        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Идет выполнение задачи");
        
        if (GetTaskInfo().PercentComplited < 100)
        {
            StartCoroutine(_enumerator());
        }
        else
        {
            UpdateStatus(TypeStatusTaskLoad.Comlite);
            _storageLog.DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Задача выполнена успешно");
        }
    }
}
