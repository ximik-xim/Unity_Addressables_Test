using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Random = UnityEngine.Random;

/// <summary>
/// Будет искать указ обьект(можно передать Key, GUID, AssetReference, IResourceLocation и т.д) в опр месте (через LoadResourceLocationsAsync)
/// (пример нужно искать обьект только локально или только на сервере)
/// </summary>
public class GetDataAddressablesTargetLocation : AbsCallbackGetDataAddressables
{
    /// <summary>
    /// Путь к фаилу, котор. и будем искать
    /// Пример,
    /// к серверу путь нач http или https
    /// к локалке путь нач Assets (пока не собрано, а после сборки обычно нач с file)
    /// 
    /// </summary>
    [SerializeField] 
    private string _pathTarget = "http / Assets / file";
    
    /// <summary>
    /// Если будет true, будут выбраны все элементы кроме тех, что совпадают с указанным именем
    /// если false, будет выбран только элемент с указанным именем
    /// </summary>
    [SerializeField] 
    private bool _excludeTarget = false;

    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
       
    /// <summary>
    /// Список Id callback, которые сейчас в ожидании
    /// (сериализован просто для удобного отслеживания в инспекторе)
    /// </summary>
    [SerializeField]
    private List<int> _idCallback = new List<int>();

    private void Awake()
    {
        _idCallback.Clear();
    
        _isInit = true;
        OnInit?.Invoke();
    }

    public override GetServerRequestData<T> GetData<T>(object data)
    {
        int id = GetUniqueId();
        CallbackRequestDataAddressablesWrapper<T> wrapperCallbackData = new CallbackRequestDataAddressablesWrapper<T>(id);
        _idCallback.Add(id);
        
        var dataCallbackLoadResource = Addressables.LoadResourceLocationsAsync(data);

        //проверяю готовы ли данные 
        if (dataCallbackLoadResource.IsDone == true)
        {
            //да готовы, начинаю обработку
            CompletedCallbackLoadResource();
        }
        else
        {
            //не, неготовы, начинаю ожидание, пока прийдут
            dataCallbackLoadResource.Completed += OnCompletedCallbackLoadResource;
        }


        void OnCompletedCallbackLoadResource(AsyncOperationHandle<IList<IResourceLocation>> obj)
        {
            //Если данные пришли
            if (dataCallbackLoadResource.IsDone == true)
            {
                dataCallbackLoadResource.Completed -= OnCompletedCallbackLoadResource;
                //начинаю обработку данных
                CompletedCallbackLoadResource();
            }

        }

        void CompletedCallbackLoadResource()
        {
            if (dataCallbackLoadResource.Status == AsyncOperationStatus.Succeeded && dataCallbackLoadResource.Result != null && dataCallbackLoadResource.Result.Count > 0)
            {
                //получили список всех путей для этого обьекта(как локальных(local), так и пути к серверу(Remote))
                var resourceAllLocation = dataCallbackLoadResource.Result;

                IResourceLocation _resLocation = null;
                
                //ищем подход. путь
                foreach (var VARIABLE in resourceAllLocation)
                {
                    if (_excludeTarget == true) 
                    {
                        if (VARIABLE.InternalId.StartsWith(_pathTarget) == false)
                        {
                            _resLocation = VARIABLE;
                            break;
                        }
                    }
                    else
                    {
                        if (VARIABLE.InternalId.StartsWith(_pathTarget) == true)
                        {
                            _resLocation = VARIABLE;
                            break;
                        }
                    }
                }

                //если нашли похдход. путь к обьекту, то нач. его загр
                if (_resLocation != null)
                {
                    StartLoadAsset(_resLocation);    
                }
                else
                {
                    //а если такого пути нету отвеч. Error
                    
                    //заполняю данные для ответа
                    wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                    wrapperCallbackData.Data.GetData = default;

                    wrapperCallbackData.Data.IsGetDataCompleted = true;
                    wrapperCallbackData.Data.Invoke();
                    
                    _idCallback.Remove(wrapperCallbackData.Data.IdMassage);
                }
                
                
                void StartLoadAsset(IResourceLocation resourceLocation)
                {
                    var dataCallbackLoadAsset = Addressables.LoadAssetAsync<T>(resourceLocation);

                    //проверяю готовы ли данные 
                    if (dataCallbackLoadAsset.IsDone == true)
                    {
                        //да готовы, начинаю обработку
                        CompletedCallbackLoadAsset();
                    }
                    else
                    {
                        //не, неготовы, начинаю ожидание, пока прийдут
                        dataCallbackLoadAsset.Completed += OnCompletedCallbackLoadAsset;
                    }

                    void OnCompletedCallbackLoadAsset(AsyncOperationHandle<T> obj)
                    {
                        //Если данные пришли
                        if (dataCallbackLoadAsset.IsDone == true)
                        {
                            dataCallbackLoadAsset.Completed -= OnCompletedCallbackLoadAsset;
                            //начинаю обработку данных
                            CompletedCallbackLoadAsset();
                        }
                    }

                    void CompletedCallbackLoadAsset()
                    {
                        if (dataCallbackLoadAsset.Status == AsyncOperationStatus.Succeeded)
                        {
                            wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Ok;
                            wrapperCallbackData.Data.GetData = dataCallbackLoadAsset.Result;

                            wrapperCallbackData.Data.IsGetDataCompleted = true;
                            wrapperCallbackData.Data.Invoke();

                            _idCallback.Remove(wrapperCallbackData.Data.IdMassage);
                            return;
                        }
                        else
                        {
                            //заполняю данные для ответа
                            wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                            wrapperCallbackData.Data.GetData = default;

                            wrapperCallbackData.Data.IsGetDataCompleted = true;
                            wrapperCallbackData.Data.Invoke();

                            _idCallback.Remove(wrapperCallbackData.Data.IdMassage);
                            return;
                        }
                    }
                }
            }
            else
            {
                //заполняю данные для ответа
                wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                wrapperCallbackData.Data.GetData = default;

                wrapperCallbackData.Data.IsGetDataCompleted = true;
                wrapperCallbackData.Data.Invoke();

                _idCallback.Remove(wrapperCallbackData.Data.IdMassage);
                return;
            }

        }

        return wrapperCallbackData.DataGet;
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
