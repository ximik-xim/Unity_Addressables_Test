using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// От этого класса будут наследоваться и реализовываться дейсвия со спец. логикой.
/// У каждого действия есть контекст, для дополнитейльно спец. логики перед выполнение действия
/// </summary>
public abstract class DKOTargetAction : MonoBehaviour
{    
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;
    
    protected virtual void LocalAwake()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public virtual DKODataRund Run(DKODataRund dkoDataRund = null)
    {
        return InvokeRun();
    }
    
    protected abstract DKODataRund InvokeRun();
}

