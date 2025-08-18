using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TL_TaskReadyExecutorDontBuffer : TL_TaskInit
{
    
    // в этой реализации списка действий нету буфера, но в замен будет дольше выполняться
    //!!!!НЕ ИСПОЛЬЗОВАТЬ ЕСЛИ В СПИСКЕ МНОГО ЭЛЕМЕНТОВ, т.к проходов будет много по одному списку
    // и тут так же не поочередно приходят ответы и не жду предущую операцию что бы начать следующую

    public event Action OnCompleted;
    public bool IsCompleted => _isCompleted;
    private bool _isCompleted = true;
    
    private bool _isStart = false;
    
    public void StartAction()
    {
        if (_isCompleted == true)
        {
            _isCompleted = false;
            
            //теперь буду просто блокировать проверку готовности задачи пока не отработает метод StartLogic у всех задач
            _isStart = true;
            foreach (var VARIABLE in _listAction)
            {
                VARIABLE.StartLogic();

                if (VARIABLE.IsCompletedLogic == false)
                {
                    VARIABLE.OnCompletedLogic += CheckCompleted;
                }
                
                // if (VARIABLE.IsCompletedLogic == false)
                // {
                //     //Тут эта реализация безопасна, т.к нету буффера и в случае чего сразу буду проходиться по заполненому списку
                //     //Но тут другой баг может произойти с тем, что все элементы могут не успеть сборсить bool IsCompleted
                //      по этому нужно сначало пройтись StartLogic а только после делать подписку и делать отдельно в конце обяз. проверку
                //     VARIABLE.OnCompletedLogic += CheckCompleted;
                //     completed = false;
                // }
            }
            _isStart = false;
            foreach (var VARIABLE in _listAction)
            {
                if (VARIABLE.IsCompletedLogic == false)
                {
                    VARIABLE.OnCompletedLogic += CheckCompleted;
                }
            }

            CheckCompleted();
        }
    }


    private void CheckCompleted()
    {
        if (_isStart == false) 
        {
            //Отписываюсь ото всех кто закончил выполнение операции
            for (int i = 0; i < _listAction.Count; i++)
            {
                if (_listAction[i].IsCompletedLogic == true)
                {
                    _listAction[i].OnCompletedLogic -= CheckCompleted;
                
                }
            }
        
            //Еще раз прохожусь и ищю есть ли те кого еще ожидаем пока выполниться операция
            for (int i = 0; i < _listAction.Count; i++)
            {
                if (_listAction[i].IsCompletedLogic == false)
                {
                    return;
                }
            }
        
            //Если нет тех, кто не закончил свою операцию, значит все конец(хэпи енд)
            Completed();
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
