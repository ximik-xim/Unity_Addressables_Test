using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// В место этого класса может быть любой друго(даже не наследник), просто для примера
/// </summary>
public class TL_ExampleReplicationTask : TL_ExampleAbsReplicationTask<string>
{
    [SerializeField] 
    private float _secondComleted = 1f;
    private IEnumerator _enumeratorCompleted;


    public override bool IsCompletedLogic => _isCompletedLogic;
    [SerializeField]
    private bool _isCompletedLogic = false;
    public override event Action OnCompletedLogic;
    
    public override void StartLogic(string data)
    {
        Debug.Log(data);
        
        _isCompletedLogic = false;
        
        _enumeratorCompleted = StartCompletedTask();
        StartCoroutine(_enumeratorCompleted);
    }
    
    private IEnumerator StartCompletedTask()
    {
        yield return new WaitForSeconds(_secondComleted);
        
        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
    
    private void OnDestroy()
    {

        if (_enumeratorCompleted != null) 
        {
            StopCoroutine(_enumeratorCompleted);
        }
    }
}
