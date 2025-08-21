using System;
using UnityEngine;

/// <summary>
/// Наслучай, если вручную будем задовать доступность обьекта(через инспектор)
/// </summary>
public class IsGetAddressablesObjectDefault : AbsBoolIsGetAddressablesObject
{
    [SerializeField] 
    private bool _isGet;

    public override bool IsInit => true;
    public override event Action OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override bool IsGet(object data)
    {
        return _isGet;
    }
}
