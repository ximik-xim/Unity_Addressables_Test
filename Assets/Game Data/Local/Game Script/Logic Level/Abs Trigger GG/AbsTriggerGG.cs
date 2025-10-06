using System;
using UnityEngine;

public abstract class AbsTriggerGG : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }

    public abstract event Action OnTriggerGG;
}
