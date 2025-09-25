using System;
using UnityEngine;

public class GetDataAddressablesErrorContinueWrapper : AbsCallbackGetDataAddressables
{
    public override bool IsInit => _absGetData.IsInit;
    public override event Action OnInit
    {
        add
        {
            _absGetData.OnInit += value;
        }

        remove
        {
            _absGetData.OnInit -= value;
        }
    }
    
    [SerializeField]
    private GetDataAddressablesErrorContinue _absGetData;

    private void Awake()
    {
        _absGetData.StartInit();
    }


    public override GetServerRequestData<T> GetData<T>(object data)
    {
        return _absGetData.GetData<T>(data);
    }
}
