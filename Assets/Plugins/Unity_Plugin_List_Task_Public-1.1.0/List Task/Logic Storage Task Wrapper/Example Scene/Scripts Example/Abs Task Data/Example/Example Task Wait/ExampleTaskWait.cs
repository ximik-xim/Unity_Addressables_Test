
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пример таски с ожиданием
/// </summary>
public class ExampleTaskWait : LT_AbsTaskWrapperDefault
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;

    [SerializeField] 
    private float _secondInit = 1f;
    private IEnumerator _enumeratorInit;
    
    [SerializeField] 
    private float _secondComleted = 1f;
    private IEnumerator _enumeratorCompleted;


    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = false;
    public override event Action OnCompletedLogic;


    public override void StartInit()
    {
        _enumeratorInit = StartInitTask();
        StartCoroutine(_enumeratorInit);
    }
    
    public override void StartLogic()
    {
        _isCompletedLogic = false;
        
        Debug.Log("Начало выполнение задачи. Ждите");
        
        _enumeratorCompleted = StartCompletedTask();
        StartCoroutine(_enumeratorCompleted);
    }

    private IEnumerator StartInitTask()
    {
        yield return new WaitForSeconds(_secondInit);
        
        _isInit = true;
        OnInit?.Invoke();
    }

    private IEnumerator StartCompletedTask()
    {
        yield return new WaitForSeconds(_secondComleted);
        
        Debug.Log("Выполнение задачи закончено");
        
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
    
    private void OnDestroy()
    {
        if (_enumeratorInit != null) 
        {
            StopCoroutine(_enumeratorInit);
        }

        if (_enumeratorCompleted != null) 
        {
            StopCoroutine(_enumeratorCompleted);
        }
    }
}
