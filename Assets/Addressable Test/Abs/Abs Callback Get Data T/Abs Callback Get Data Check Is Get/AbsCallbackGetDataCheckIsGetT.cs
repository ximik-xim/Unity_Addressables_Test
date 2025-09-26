using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Нужен для проверки, можно ли взять этот обьект(к примеру с локального хран)
/// </summary>
[System.Serializable]
public class AbsCallbackGetDataCheckIsGetT<AbsCallbackGet, ArgData, CheckBool>  
    where AbsCallbackGet : AbsCallbackGetDataMethodT<ArgData> 
    where CheckBool : AbsRequestDataBoolIsGetObject<ArgData>
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField] 
    private AbsCallbackGet _absGetData;

    [SerializeField] 
    private CheckBool _absIsGetObject;

    /// <summary>
    /// Список Id callback, которые сейчас в ожидании
    /// (сериализован просто для удобного отслеживания в инспекторе)
    /// </summary>
    [SerializeField]
    private List<int> _idCallback = new List<int>();

    public void StartInit()
    {
        if (_absIsGetObject.IsInit == false)
        {
            _absIsGetObject.OnInit += OnInitAbsIsGet;
            return;
        }

        InitAbsIsGet();
        
    }
    
    private void OnInitAbsIsGet()
    {
        if (_absIsGetObject.IsInit == true)
        {
            _absIsGetObject.OnInit -= OnInitAbsIsGet;
            InitAbsIsGet();
        }
    }
    
    private void InitAbsIsGet()
    {
        if (_isInit == false)
        {
            _isInit = true;
            OnInit?.Invoke();    
        }
    }
    
    public GetServerRequestData<T> GetData<T>(ArgData data)
    {
        int id = GetUniqueId();
        CallbackRequestDataWrapperT<T> callbackData = new CallbackRequestDataWrapperT<T>(id);
        _idCallback.Add(id);
        
        var callback = _absIsGetObject.IsGet(data);

        if (callback.IsGetDataCompleted == true)
        {
            ComplitedIsGet();
        }
        else
        {
            callback.OnGetDataCompleted += OnComplitedIsGet;
        }
        
        void OnComplitedIsGet()
        {

            if (callback.IsGetDataCompleted == true) 
            {
                callback.OnGetDataCompleted -= OnComplitedIsGet;
                ComplitedIsGet();
            }
            
        }
        
        void ComplitedIsGet()
        {
            if (callback.GetData == true)
            {
                StartGetData();
            }
            else
            {
                CallbackError();
            }
        }

        
        void StartGetData()
        {
            Debug.Log("Обьект разрешено брать");
            
            var callbackGetData = _absGetData.GetData<T>(data);
            
            if (callbackGetData.IsGetDataCompleted == true)
            {
                ComplitedGetData();
            }
            else
            {
                callbackGetData.OnGetDataCompleted += OnComplitedGetData;
            }
        
            void OnComplitedGetData()
            {

                if (callbackGetData.IsGetDataCompleted == true) 
                {
                    callbackGetData.OnGetDataCompleted -= OnComplitedGetData;
                    ComplitedGetData();
                }
            
            }
        
            void ComplitedGetData()
            {
                callbackData.Data.StatusServer = callbackGetData.StatusServer;
                callbackData.Data.GetData = callbackGetData.GetData;

                callbackData.Data.IsGetDataCompleted = true;
                callbackData.Data.Invoke();
                
                _idCallback.Remove(callbackData.Data.IdMassage);
                return;
            }
            
        }

        void CallbackError()
        {
            Debug.Log("Обьект запрещено брать");
            //Если запрещено брать, то возращаем пустышку
            //тут именно ERROR
            callbackData.Data.StatusServer = StatusCallBackServer.Error;
            callbackData.Data.GetData = default;

            callbackData.Data.IsGetDataCompleted = true;
            callbackData.Data.Invoke();
            
            _idCallback.Remove(callbackData.Data.IdMassage);
        }
        
        return callbackData.DataGet;
    }
    
    private int GetUniqueId()
    {
        int id = 0;
        while (true)
        {
            id = Random.Range(0, Int32.MaxValue - 1);
            if (_idCallback.Contains(id) == false)
            {
                break;
            }
        }

        return id;
    }
}
