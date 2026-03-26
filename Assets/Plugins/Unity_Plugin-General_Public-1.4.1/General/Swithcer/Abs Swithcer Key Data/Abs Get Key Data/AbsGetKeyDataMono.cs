using System;
using UnityEngine;

public abstract class AbsGetKeyDataMono<Key, KeyData> : MonoBehaviour where KeyData : AbsGetKeyData<Key>
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract KeyData GetKeyData();
    
}
