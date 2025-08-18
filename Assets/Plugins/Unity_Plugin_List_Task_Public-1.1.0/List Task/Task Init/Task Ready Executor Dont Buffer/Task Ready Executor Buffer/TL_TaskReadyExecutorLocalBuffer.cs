using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TL_TaskReadyExecutorLocalBuffer : TL_TaskInit
{
    public bool IsCompleted => _isCompleted;
    private bool _isCompleted = true;
    
    public event Action OnCompleted;
   
//в этой реализации буффер находиться как локальная переменная, за счет чего нету не нужный глобальных переменных
    public void StartAction()
    {
        List<LT_AbsTask> _buffer = new List<LT_AbsTask>();
        bool _isStart = false;
        
        StartLogic();
        
        void StartLogic()
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
                //теперь буду просто блокировать проверку готовности задачи пока не отработает метод StartLogic у всех задач
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

        void CheckCompleted()
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
