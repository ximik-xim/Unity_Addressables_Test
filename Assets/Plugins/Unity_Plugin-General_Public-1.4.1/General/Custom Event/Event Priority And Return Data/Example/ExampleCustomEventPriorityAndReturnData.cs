using System;
using UnityEngine;

/// <summary>
/// Пример класса с event
/// </summary>
public class ExampleCustomEventPriorityAndReturnData : MonoBehaviour
{
    /// <summary>
    /// Хост который содержит обертку и сам класс для работы event
    /// </summary>
    private HostCustomEventPriorityAndReturnData<string, CustomEventPriorityExampleData> OnEventUpdateData = new HostCustomEventPriorityAndReturnData<string, CustomEventPriorityExampleData>();

    /// <summary>
    /// Обертка с инкопсулированными методами 
    /// </summary>
    public WrapperCustomEventPriorityAndReturnData<string, CustomEventPriorityExampleData> OnUpdateData => OnEventUpdateData.WrapperCustomEventPriorityT;

    private void Awake()
    {
        OnEventUpdateData.CustomEventPriorityT.OnReturnDataEvent += OnReturnDataEvent;
    }

    private void OnReturnDataEvent(string arg1, CustomEventPriorityAndReturnData<string, CustomEventPriorityExampleData>.ActionCustomPriorityReturnData<CustomEventPriorityExampleData> arg2, int arg3)
    {
        Debug.Log("Подписчик при вызове event вернул = " + arg1);
    }

    private void Start()
    { 
        OnEventUpdateData.CustomEventPriorityT.Invoke(new CustomEventPriorityExampleData());
    }

    private void OnDestroy()
    {
        OnEventUpdateData.CustomEventPriorityT.OnReturnDataEvent -= OnReturnDataEvent;
    }
}
