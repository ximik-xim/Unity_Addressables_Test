using System;
using UnityEngine;

/// <summary>
/// Пример подписчика
/// </summary>
public class ExampleSubscribeCustomEventPriorityT : MonoBehaviour
{
    
    [SerializeField]
    private ExampleCustomEventPriorityT _exampleCustomEvent;
    
    /// <summary>
    /// Приоритет выполнения в подписке
    /// (будет определять каким в очереди выполнить этот элемент)
    /// </summary>
    [SerializeField]
    private int _priority;
    
    private void Awake()
    {
        _exampleCustomEvent.OnUpdateData.Subscribe(Test, _priority);
    }

    private void Test(CustomEventPriorityExampleData obj)
    {
        Debug.Log("Invoke Event T " + this.gameObject.name);
    }

    private void OnDestroy()
    {
        _exampleCustomEvent.OnUpdateData.Unsubscribe(Test, _priority);
    }
}
