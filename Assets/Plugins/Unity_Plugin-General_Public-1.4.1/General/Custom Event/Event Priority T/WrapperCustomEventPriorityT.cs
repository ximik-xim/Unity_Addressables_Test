using System;
using UnityEngine;

/// <summary>
/// Обертка нужна только для инкапсуляции методов
/// </summary>
public class WrapperCustomEventPriorityT<InvokeType>
{
    public WrapperCustomEventPriorityT(CustomEventPriorityT<InvokeType> customEventPriorityT)
    {
        _customEventPriorityT = customEventPriorityT;
    }

    private CustomEventPriorityT<InvokeType> _customEventPriorityT;

    public bool IsBlock => _customEventPriorityT.IsBlock;

    public event Action OnUpdateStatusBlock
    {
        add
        {
            _customEventPriorityT.OnUpdateStatusBlock += value;
        }
        remove
        {
            _customEventPriorityT.OnUpdateStatusBlock -= value;
        }
    }

    public void Subscribe(Action<InvokeType> actionCustom, int priority = Int32.MaxValue - 1)
    {
        _customEventPriorityT.Subscribe(actionCustom, priority);
    }

    public void Unsubscribe(Action<InvokeType> actionCustom, int priority)
    {
        _customEventPriorityT.Unsubscribe(actionCustom, priority);
    }

    public void UnsubscribeAllPriority(Action<InvokeType> actionCustom)
    {
        _customEventPriorityT.UnsubscribeAllPriority(actionCustom);
    }
}
