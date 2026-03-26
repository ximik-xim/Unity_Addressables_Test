using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Пример задачи(в которую перед. ссылка на DKO) для списка задач
/// </summary>
public class TL_ExampleTaskDKO : TL_AbsTaskLogicDKO
{
    public override event Action OnInit;
    public override bool IsInit => true;
    
    [SerializeField] 
    private float _secondComleted = 1f;
    private IEnumerator _enumeratorCompleted;

    public override bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = false;
    public override event Action OnCompletedLogic;
    
    public override void StartLogic(DKOKeyAndTargetAction dataDKO)
    {
        _isCompletedLogic = false;
        
        Debug.Log("Начало выполнение задачи. Ждите");
        
        _enumeratorCompleted = StartCompletedTask();
        StartCoroutine(_enumeratorCompleted);
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
        if (_enumeratorCompleted != null) 
        {
            StopCoroutine(_enumeratorCompleted);
        }
    }
}
