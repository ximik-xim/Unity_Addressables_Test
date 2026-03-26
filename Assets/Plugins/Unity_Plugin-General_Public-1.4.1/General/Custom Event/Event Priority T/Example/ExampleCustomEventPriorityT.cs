using System;
using UnityEngine;

/// <summary>
/// Пример класса с event
/// </summary>
public class ExampleCustomEventPriorityT : MonoBehaviour
{
    /// <summary>
    /// Хост который содержит обертку и сам класс для работы event
    /// </summary>
    private HostCustomEventPriorityT<CustomEventPriorityExampleData> OnEventUpdateData = new HostCustomEventPriorityT<CustomEventPriorityExampleData>();
    /// <summary>
    /// Обертка с инкопсулированными методами 
    /// </summary>
    public WrapperCustomEventPriorityT<CustomEventPriorityExampleData> OnUpdateData => OnEventUpdateData.WrapperCustomEventPriorityT;

    private void Start()
    { 
        OnEventUpdateData.CustomEventPriorityT.Invoke(new CustomEventPriorityExampleData());
    }
}
