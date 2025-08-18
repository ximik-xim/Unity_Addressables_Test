using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class TL_TaskInit 
{
    [SerializeField]
    public List<LT_AbsTask> _listAction;
    
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    public event Action OnInit;

    public void StartInit()
    {
        List<LT_AbsTask> _buffer = new List<LT_AbsTask>();
        bool _isStart = false;
        
        StartLogic();

        void StartLogic()
        {
            if (_isInit == false)
            {
                _isStart = true;

                foreach (var VARIABLE in _listAction)
                {
                    //нужно именно 2 проверки
                    //1 - что бы не вызвать Init просто так(обьект может сам инициализироваться отдельно)
                    //2 - что бы проверить инициализировался ли обьект или его нужно ждать
                    if (VARIABLE.IsInit == false)
                    {
                        VARIABLE.StartInit();

                        if (VARIABLE.IsInit == false)
                        {
                            _buffer.Add(VARIABLE);
                            VARIABLE.OnInit += CheckInitCompleted;
                        }
                    }
                }

                _isStart = false;

                CheckInitCompleted();
            }
        }

        void CheckInitCompleted()
        {
            if (_isStart == false) 
            {
                int targetCount = _buffer.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (_buffer[i].IsInit == true)
                    {
                        _buffer[i].OnInit -= CheckInitCompleted;
                        _buffer.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (_buffer.Count == 0)
                {
                    InitCompleted();
                }
            }
        }
    }

    private void InitCompleted()
    {
        _isInit = true;
        OnInit?.Invoke();
    }
}
