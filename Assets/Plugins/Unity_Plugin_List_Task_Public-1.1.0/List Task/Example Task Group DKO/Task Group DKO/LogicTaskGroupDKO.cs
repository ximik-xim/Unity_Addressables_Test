using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// группа из списков задач
/// Выполнение задач идет следующим образом
/// - Сначало список задач _beforeTask
/// - Следом список задач _mainTask
/// - И в конце список задач _afterTask
/// </summary>
public abstract class LogicTaskGroupDKO : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    public event Action OnCompleted;
    public bool IsCompleted => _isCompleted;
    private bool _isCompleted = true;
    
    [SerializeField]
    private LogicListTaskDKO _beforeTask;

    [SerializeField]
    private LogicListTaskDKO _mainTask;
    
    [SerializeField]
    private LogicListTaskDKO _afterTask;
    
    private DKOKeyAndTargetAction _bufferDKO;
    
    /// <summary>
    /// Желательно вызывать в Awake
    /// </summary>
    protected void StartInit()
    {
        if (_beforeTask.IsInit == false)
        {
            _beforeTask.OnInit += OnInitBeforeTask;
        }
        
        if (_mainTask.IsInit == false)
        {
            _mainTask.OnInit += OnInitMainTask;
        }
        
        if (_afterTask.IsInit == false)
        {
            _afterTask.OnInit += OnInitAfterTask;
        }

        InitCheck();
    }

    private void OnInitBeforeTask()
    {
        _beforeTask.OnInit -= OnInitBeforeTask;
        InitCheck();
    }

    private void OnInitMainTask()
    {
        _mainTask.OnInit -= OnInitMainTask;
        InitCheck();
    }

    private void OnInitAfterTask()
    {
        _afterTask.OnInit -= OnInitAfterTask;
        InitCheck();
    }

    private void InitCheck()
    {
        if (_isInit == false) 
        {
            if (_beforeTask.IsInit == true && _mainTask.IsInit == true && _afterTask.IsInit == true)
            {
                _isInit = true;
                OnInit?.Invoke();
            }
        }
       
    }

    public void StartAction(DKOKeyAndTargetAction tileDKO)
    {
        if (_isCompleted == true)
        {
            _isCompleted = false;
            _bufferDKO = tileDKO;

            _beforeTask.OnCompleted += OnCompletedBeforeTask;
            _beforeTask.StartAction(_bufferDKO);
            
        }
        
    }

    private void OnCompletedBeforeTask()
    {
        _beforeTask.OnCompleted -= OnCompletedBeforeTask;
        
        _mainTask.OnCompleted += OnCompletedMainTask;
        _mainTask.StartAction(_bufferDKO);
        
    }

    private void OnCompletedMainTask()
    {
        _mainTask.OnCompleted -= OnCompletedMainTask;
        
        _afterTask.OnCompleted += OnCompletedAfterTask;
        _afterTask.StartAction(_bufferDKO);
    }

    private void OnCompletedAfterTask()
    {
        _afterTask.OnCompleted -= OnCompletedAfterTask;

        _isCompleted = true;
        OnCompleted?.Invoke();
    }
    
}
