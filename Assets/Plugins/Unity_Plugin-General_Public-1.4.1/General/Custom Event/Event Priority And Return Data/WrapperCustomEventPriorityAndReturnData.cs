using System;
using UnityEngine;

/// <summary>
/// Обертка нужна только для инкапсуляции методов
/// </summary>
public class WrapperCustomEventPriorityAndReturnData<ReturnType, InvokeType> : MonoBehaviour
{
    public WrapperCustomEventPriorityAndReturnData(CustomEventPriorityAndReturnData<ReturnType, InvokeType> customEventPriorityT)
    {
        _customEventPriorityT = customEventPriorityT;
    }

    private CustomEventPriorityAndReturnData<ReturnType, InvokeType> _customEventPriorityT;

    public bool IsBlock => _customEventPriorityT.IsBlock;
    
    public event Action<ReturnType, CustomEventPriorityAndReturnData<ReturnType,InvokeType>.ActionCustomPriorityReturnData<InvokeType>, int> OnReturnDataEvent
    {
        add
        {
            _customEventPriorityT.OnReturnDataEvent += value;
        }
        remove
        {
            _customEventPriorityT.OnReturnDataEvent -= value;
        }
    }
    
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

    public void Subscribe(CustomEventPriorityAndReturnData<ReturnType,InvokeType>.ActionCustomPriorityReturnData<InvokeType> actionCustom, int priority = Int32.MaxValue - 1)
    {
        _customEventPriorityT.Subscribe(actionCustom, priority);
    }

    public void Unsubscribe(CustomEventPriorityAndReturnData<ReturnType,InvokeType>.ActionCustomPriorityReturnData<InvokeType> actionCustom, int priority)
    {
        _customEventPriorityT.Unsubscribe(actionCustom, priority);
    }

    public void UnsubscribeAllPriority(CustomEventPriorityAndReturnData<ReturnType,InvokeType>.ActionCustomPriorityReturnData<InvokeType> actionCustom)
    {
        _customEventPriorityT.UnsubscribeAllPriority(actionCustom);
    }
}
