using System;
using UnityEngine;

public abstract class IGetBoolStatusMono : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract IGetBoolStatus GetDataStatus();
}
