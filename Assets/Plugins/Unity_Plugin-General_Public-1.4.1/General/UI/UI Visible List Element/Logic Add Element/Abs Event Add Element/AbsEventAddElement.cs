using System;
using UnityEngine;

/// <summary>
/// Будет сообщать о том, что надо добавить N кол-во элементов
/// (а как это будет происходить решать будет наследник)
/// </summary>
public abstract class AbsEventAddElement : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract event Action<int> OnAddElement;
}
