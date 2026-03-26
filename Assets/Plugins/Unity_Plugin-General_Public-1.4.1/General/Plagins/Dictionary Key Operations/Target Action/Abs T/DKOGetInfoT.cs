using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DKOGetInfoT<T> : DKOTargetAction
{
    [SerializeField] private T _data;

    private DKODataInfoT<T> _dataGet;

    protected override void LocalAwake()
    {
        _dataGet = new DKODataInfoT<T>(_data);
        base.LocalAwake();
    }

    protected override DKODataRund InvokeRun()
    {
        if (_dataGet == null)
        {
            _dataGet = new DKODataInfoT<T>(_data);
        }

        return _dataGet;
    }

}


public class DKODataInfoT<T> : DKODataRund
{
    public DKODataInfoT(T data)
    {
        _data = data;
    }
    
    private T _data;

    public T Data => _data;
}