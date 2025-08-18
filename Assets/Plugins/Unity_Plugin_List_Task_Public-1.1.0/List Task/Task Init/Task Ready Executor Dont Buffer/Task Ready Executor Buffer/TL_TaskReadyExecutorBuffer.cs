using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TL_TaskReadyExecutorBuffer : TL_TaskInit
{
    
    //Реализация ожидание выполнения некого списка действий через буффер. В буффере находяться те операции которые еще не закончили выполнять свою логику 
    //При этом тут нету ожидания того, что бы прошлое действие закончилось и только
    //после перешли бы к следующему действию, тут в любом порядке приходят ответы 

    
    /// <summary>
    /// сериализую, что бы видить через инспектор, какая операция еще не закончила свою логику
    /// </summary>
    [SerializeField]
    private List<LT_AbsTask> _buffer = new List<LT_AbsTask>();

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
                //т.к CheckCompleted будет выполнен по любому после того как будет вызвать у всех элементов метод StartLogic,
                //то тут бага с тем что не успеют сбориться bool IsCompleted на момент проверки, просто такого бага не будет 
                VARIABLE.StartLogic();
                if (VARIABLE.IsCompletedLogic == false)
                {
                    _buffer.Add(VARIABLE);
                    VARIABLE.OnCompletedLogic += CheckCompleted;
                }
                
            }

            _isStart = false;
            //вынес подписку в отдельный проход т.к
            //если делать сразу подписку при проверки VARIABLE.IsCompletedLogic, то метод CheckCompleted может отработать до того как 
            //добавиться в список остальные элементы, а значит в списке будет 1 элемент и отработает Completed, что будет являться багом, 
            //т.к остальные элементы тупо не успели провериться
            //а таким не хитрым методом по сути блокирую вызов CheckCompleted до момента пока список не заполниться всеми элементами
            //НО ИЗ ЗА ЭТОГО НУЖНО УЧЕСТЬ 1) наличие в буффере уже выполнных задач 2)сделать доп проверку после старта
            // foreach (var VARIABLE in _buffer)
            // {
            //     VARIABLE.OnCompletedLogic += CheckCompleted;
            // }

            CheckCompleted();
        }
    }

    private void CheckCompleted()
    {
        if (_isStart == false)
        {
            int targetCount = _buffer.Count;
            for (int i = 0; i < targetCount; i++)
            {
                if (_buffer[i].IsCompletedLogic == true)
                {
                    _buffer[i].OnCompletedLogic -= CheckCompleted;
                    _buffer.RemoveAt(i);
                    i--;
                    targetCount--;
                }
            }

            if (_buffer.Count == 0)
            {
                Completed();
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
