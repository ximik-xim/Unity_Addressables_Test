using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsExceptionsLogicT<Key,Data> : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    public abstract List<AbsKeyData<Key, Data>> GetListExceptions();
}
