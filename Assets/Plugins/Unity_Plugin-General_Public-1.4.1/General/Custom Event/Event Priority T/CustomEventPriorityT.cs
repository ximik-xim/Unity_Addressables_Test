using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кастомный event с возможнстью указания приоритета для выполнения указанной задачи
/// </summary>
public class CustomEventPriorityT<InvokeType>
{
    private SortedDictionary<int, List<Action<InvokeType>>> _dictionaryAction = new SortedDictionary<int, List<Action<InvokeType>>>();

    /// <summary>
    /// Нужен что бы во время выполнения Action нельзя было добавить или удалить подписчика
    /// (это надо делать после того как закончиться выполнения (IsBlock = false))   
    /// </summary>
    public bool IsBlock => _isBlock;
    private bool _isBlock = false;
    public event Action OnUpdateStatusBlock;

    public void Subscribe(Action<InvokeType> actionCustom, int priority = Int32.MaxValue - 1)
    {
        if (_isBlock == false)
        {
            if (_dictionaryAction.TryGetValue(priority, out var list) == false)
            {
                list = new List<Action<InvokeType>>();
                _dictionaryAction.Add(priority, list);
            }

            list.Add(actionCustom);
        }
    }

    /// <summary>
    /// Отписка этого action у указанного приоритета 
    /// </summary>
    /// <param name="actionCustom"></param>
    public void Unsubscribe(Action<InvokeType> actionCustom, int priority)
    {
        if (_isBlock == false)
        {
            if (_dictionaryAction.ContainsKey(priority) == true)
            {
                _dictionaryAction[priority].Remove(actionCustom);
            }
        }
    }
    
    /// <summary>
    /// Отписка этого action у всех приоритетов 
    /// </summary>
    /// <param name="actionCustom"></param>
    public void UnsubscribeAllPriority(Action<InvokeType> actionCustom)
    {
        if (_isBlock == false) 
        {
            foreach (var list in _dictionaryAction.Values)
            {
                list.Remove(actionCustom);
            }
        }

    }

    /// <summary>
    /// Запустить выполнение action по их приоритету
    /// </summary>
    public void Invoke(InvokeType invokeData)
    {
        _isBlock = true;
        OnUpdateStatusBlock?.Invoke();
        
        foreach (var pair in _dictionaryAction)
        {
            foreach (var action in pair.Value)
            {
                action?.Invoke(invokeData);
            } 
        }
        
        _isBlock = false;
        OnUpdateStatusBlock?.Invoke();
    }
}