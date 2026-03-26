using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Получаю ключи, из других абстракций
/// (на случай, если из разных мест надо получить списки ключей)
/// </summary>
public class GetListKeyDropdownInListAbs : AbsGetListKeyDropdown
{
    [SerializeField]
    private List<AbsGetListKeyDropdown> _listKey;

    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;

    private void Awake()
    {
        StartInit();
    }
    
    private void StartInit()
    {
        List<AbsGetListKeyDropdown> _buffer = new List<AbsGetListKeyDropdown>();
        bool _isStart = false;
        
        StartLogic();

        void StartLogic()
        {
            if (_isInit == false)
            {
                _isStart = true;

                foreach (var VARIABLE in _listKey)
                {
                        if (VARIABLE.IsInit == false)
                        {
                            _buffer.Add(VARIABLE);
                            VARIABLE.OnInit += CheckInitCompleted;
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

    public override List<KeyDropdown> GetListKeyDropdown()
    {
        List<KeyDropdown> list = new List<KeyDropdown>();
        foreach (var VARIABLE in _listKey)
        {
            foreach (var VARIABLE2 in VARIABLE.GetListKeyDropdown())
            {
                list.Add(VARIABLE2);
            }
        }

        return list;
    }
}
