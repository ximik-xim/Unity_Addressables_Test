using System;
using UnityEngine;

/// <summary>
/// Нужен что бы понять, надо ли удалять этот элемент
/// </summary>
public abstract class AbsIsRemoveElement : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract bool IsRemoveElement();
}
