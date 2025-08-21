using System;
using UnityEngine;

/// <summary>
/// Делает запрос, к хранилещу с обьектами(которые или разрешено или запрещено брать)
/// </summary>
public class IsGetAddressablesObjectSOStorage : AbsBoolIsGetAddressablesObject
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField] 
    private SOStorageBoolIsGetAddressablesObject _storageBool;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override bool IsGet(object data)
    {
        return _storageBool.IsGetObject(data);
    }
}
