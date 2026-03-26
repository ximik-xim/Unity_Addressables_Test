using System;
using UnityEngine;

/// <summary>
/// Нужен что бы получить текст откуда то ...
/// </summary>
public abstract class AbsGetStringText  : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract string GetStringText();
}
 
