using System;

[System.Serializable]
public abstract class AbsGetKeyData<Key>
{
    public abstract Key GetCurrentKey { get; }
    public abstract event Action OnUpdateKey;
}
