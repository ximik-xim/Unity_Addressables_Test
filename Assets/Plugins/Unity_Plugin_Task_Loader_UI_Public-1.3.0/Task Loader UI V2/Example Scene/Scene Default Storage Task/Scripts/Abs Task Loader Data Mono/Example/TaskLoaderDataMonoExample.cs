using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пример обычной Task с логикой ожид. до моменты окончания выполнение задачи
/// </summary>
public class TaskLoaderDataMonoExample : AbsTaskLoaderDataMono
{
    [SerializeField] 
    private int _timeStopTask = 2;
    
    private void Awake()
    {
        InitTask();
    }

    protected override void StartLogic()
    {
        UpdateStatus(TypeStatusTaskLoad.Start);
        UpdateStatus(TypeStatusTaskLoad.Load);
        
        StartCoroutine(_enumerator());
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
        
        if (GetTaskInfo().PercentComplited < 100)
        {
            StartCoroutine(_enumerator());
        }
        else
        {
            UpdateStatus(TypeStatusTaskLoad.Comlite);
        }
    }
}
