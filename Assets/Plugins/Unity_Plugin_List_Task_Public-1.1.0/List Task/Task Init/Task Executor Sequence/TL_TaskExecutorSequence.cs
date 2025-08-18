using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// выполняеться по цепочке, друг за другом, дожидаясь окончания выполнения опереции у текущего элемента и только потом переходя к следующему
/// </summary>
[System.Serializable]
public class TL_TaskExecutorSequence : TL_TaskInit
{
    public event Action OnCompleted;
    public bool IsCompleted => _isCompleted;
    private bool _isCompleted = true;
    
    public void StartAction()
    {
        if (_isCompleted == true)
        {
            _isCompleted = false;

            int targetCount = _listAction.Count;
            int currenntCount = 0;

            CheckCompleted();

            void CheckCompleted()
            {
                for (int i = currenntCount; i < targetCount; i++)
                {
                    // нужно что бы, если тек. id процесса уже выполнился и начнеться след. проход в цыкле, что бы сохранить id
                    currenntCount = i;

                    _listAction[currenntCount].StartLogic();

                    if (_listAction[currenntCount].IsCompletedLogic == false)
                    {
                        //Тут эта реализация безопасна, т.к нету буффера и в случае чего сразу буду проходиться по заполненому списку
                        //и т.к тут поочереди все выполняеться и нету проверки у всех задач статуса bool IsCompleted
                        //а только у текущей, то проблем не вижу тут 
                        _listAction[currenntCount].OnCompletedLogic -= OnCompletedCurrentLogic;
                        _listAction[currenntCount].OnCompletedLogic += OnCompletedCurrentLogic;
                        return;
                    }
                }

                Completed();
            }

            void OnCompletedCurrentLogic()
            {
                _listAction[currenntCount].OnCompletedLogic -= OnCompletedCurrentLogic;

                //нужно, что бы не проверять этот же Id 
                currenntCount++;

                CheckCompleted();
            }
        }
        
    }
    
    private void Completed()
    {
        if (_isCompleted == false)
        {
            _isCompleted = true;
            OnCompleted?.Invoke();    
        }
    }
}
