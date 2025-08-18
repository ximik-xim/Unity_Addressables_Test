using System;
using UnityEngine;

public abstract class AbsGetParent : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract GameObject GetParent();
}
