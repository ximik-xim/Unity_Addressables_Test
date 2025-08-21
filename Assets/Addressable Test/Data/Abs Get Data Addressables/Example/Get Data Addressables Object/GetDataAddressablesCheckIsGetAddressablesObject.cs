using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Нужен для проверки, можно ли взять этот обьект(к примеру с локального хран)
/// </summary>
public class GetDataAddressablesCheckIsGetAddressablesObject : AbsCallbackGetDataAddressables
{
    [SerializeField] 
    private AbsCallbackGetDataAddressables _absGetDataAddressables;
    
    [SerializeField] 
    private AbsBoolIsGetAddressablesObject _absIsGetAddressablesObject;


    public override bool IsInit => true;
    public override event Action OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override GetServerRequestData<T> GetData<T>(object data)
    {
        if (_absIsGetAddressablesObject.IsGet(data) == true)
        {
            return _absGetDataAddressables.GetData<T>(data);
        }

        //Если запрещено брать, то возращаем пустышку
        CallbackRequestDataAddressablesWrapper<T> callbackData = new CallbackRequestDataAddressablesWrapper<T>(0);
        
        //тут именно ERROR
        callbackData.Data.StatusServer = StatusCallBackServer.Error;
        callbackData.Data.GetData = default;

        callbackData.Data.IsGetDataCompleted = true;
        callbackData.Data.Invoke();


        return callbackData.DataGet;
    }
}
