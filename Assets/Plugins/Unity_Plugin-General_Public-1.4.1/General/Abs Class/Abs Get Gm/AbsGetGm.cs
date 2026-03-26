using System;
using UnityEngine;
/// <summary>
/// Нужен что бы получить GM откуда то ...
/// </summary>
public abstract class AbsGetGm : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract GameObject GetGm();
}
