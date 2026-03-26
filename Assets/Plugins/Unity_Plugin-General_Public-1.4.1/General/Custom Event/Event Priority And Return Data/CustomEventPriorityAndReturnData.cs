using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кастомный event с возможнстью указания приоритета для выполнения указанной задачи
/// А так же с возращением типов данных при выполнении Action
/// </summary>
public class CustomEventPriorityAndReturnData<ReturnType, InvokeType>
{
    /// <summary>
    /// Создаю отдельный делегат, что бы event по окончанию выполнения работы возращал какие то данные
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate ReturnType ActionCustomPriorityReturnData<in T>(T obj);

    /// <summary>
    /// Возращает данные которые передал event кода был вызван
    /// </summary>
    public event Action<ReturnType, ActionCustomPriorityReturnData<InvokeType>, int> OnReturnDataEvent;
    
    private SortedDictionary<int, List<ActionCustomPriorityReturnData<InvokeType>>> _dictionaryAction = new SortedDictionary<int, List<ActionCustomPriorityReturnData<InvokeType>>>();

    /// <summary>
    /// Нужен что бы во время выполнения Action нельзя было добавить или удалить подписчика
    /// (это надо делать после того как закончиться выполнения (IsBlock = false))   
    /// </summary>
    public bool IsBlock => _isBlock;
    private bool _isBlock = false;
    public event Action OnUpdateStatusBlock; 
    
    public void Subscribe(ActionCustomPriorityReturnData<InvokeType> actionCustom, int priority = Int32.MaxValue - 1)
    {
        if (_isBlock == false)
        {
            if (_dictionaryAction.TryGetValue(priority, out var list) == false)
            {
                list = new List<ActionCustomPriorityReturnData<InvokeType>>();
                _dictionaryAction.Add(priority, list);
            }

            list.Add(actionCustom);
        }
    }

    /// <summary>
    /// Отписка этого action у указанного приоритета 
    /// </summary>
    /// <param name="actionCustom"></param>
    public void Unsubscribe(ActionCustomPriorityReturnData<InvokeType> actionCustom, int priority)
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
    public void UnsubscribeAllPriority(ActionCustomPriorityReturnData<InvokeType> actionCustom)
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
                ReturnType returnData = default;

                if (action != null)
                {
                    returnData = action.Invoke(invokeData);
                }
                
                OnReturnDataEvent?.Invoke(returnData, action, pair.Key);
            } 
        }

        _isBlock = false;
        OnUpdateStatusBlock?.Invoke();
    }
}
