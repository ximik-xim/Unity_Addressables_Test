using System;
using UnityEngine;

/// <summary>
/// Будет сообщать о том, что надо запустить логику для удаление элементов
/// (а как это будет происходить определение, каких элементов и как будет происходить удаление будет определять наследник)
/// </summary>
public abstract class AbsEventRemoveElement : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract event Action OnRemoveElement;
}
