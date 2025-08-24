using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

/// <summary>
/// В этой реализации, запрашиваю обновление всех каталогов разом(единым пакетом)
/// (сразу с логикой переотпр. запроса, в случае не удачи)
/// </summary>
public class UpdateCatalogsAllErrorContinue : AbsUpdateCatalogs
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
    
    [SerializeField]
    private LogicErrorCallbackRequestAddressables _errorLogic;
    
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
    
    public override GetServerRequestData<StorageStatusCallbackIResourceLocation> StartUpdateCatalog(List<string> idCatalogUpdate)
    {
        //запрашиваю данные
        var dataCallback = Addressables.UpdateCatalogs(idCatalogUpdate);
        
        int id = GetUniqueId();
        //делаю обертку т.к могу несколько раз делать запросы на данные, а верну лиш 1 итог. результат 
        CallbackStorageStatusIResourceLocationAddressablesWrapper wrapperCallbackData = new CallbackStorageStatusIResourceLocationAddressablesWrapper(id);
        _idCallback.Add(id);

        //проверяю готовы ли данные 
        if (dataCallback.IsDone == true)
        {
            //да готовы, начинаю обработку
            CompletedCallback();
        }
        else
        {
            //не, неготовы, начинаю ожидание, пока прийдут
            dataCallback.Completed += OnCompletedCallback;
        }
        
        void OnCompletedCallback(AsyncOperationHandle<List<IResourceLocator>> obj)
        {
            //Если данные пришли
            if (dataCallback.IsDone == true)
            {
                dataCallback.Completed -= OnCompletedCallback;
                //начинаю обработку данных
                CompletedCallback();    
            }
        }

        void CompletedCallback()
        {
            //Если успешно получил данные
            if (dataCallback.Status == AsyncOperationStatus.Succeeded)
            {
                //очищаю список ошибок
                _errorLogic.OnRemoveAllError();
                
                //заполняю данные для ответа
                wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Ok;

                List<StatusCallbackIResourceLocation> listStatusCallback = new List<StatusCallbackIResourceLocation>();
                foreach (var VARIABLE in dataCallback.Result)
                {
                    foreach (var VARIABLE2 in VARIABLE.AllLocations)
                    {
                        StatusCallbackIResourceLocation statusCallback = new StatusCallbackIResourceLocation(StatusCallBackServer.Ok, VARIABLE2);
                        listStatusCallback.Add(statusCallback);
                    }
                }

                StorageStatusCallbackIResourceLocation statusAll = new StorageStatusCallbackIResourceLocation(listStatusCallback, TypeStorageStatusCallbackIResourceLocator.Ok);
                
                wrapperCallbackData.Data.GetData = statusAll;
                wrapperCallbackData.Data.IsGetDataCompleted = true;
                wrapperCallbackData.Data.Invoke();
                
                _idCallback.Remove(wrapperCallbackData.DataGet.IdMassage);
                return;
            }
            else
            {
                //добавляю ошибку
                _errorLogic.OnAddError();
                
                //Проверяю, могу ли еще раз отпр. запрос
                if (_errorLogic.IsContinue == true) 
                {
                    //заного отпр. запрос, и по новой 
                    dataCallback = Addressables.UpdateCatalogs(idCatalogUpdate);
                    if (dataCallback.IsDone == true)
                    {
                        CompletedCallback();
                    }
                    else
                    {
                        dataCallback.Completed -= OnCompletedCallback;
                        dataCallback.Completed += OnCompletedCallback;
                    }
                    
                    return;
                }
                else
                {
                    //если попытки достучаться до сервера закончились, то отпр. все как есть(ошибку)
                    
                    //очищаю список ошибок
                    _errorLogic.OnRemoveAllError();
                    
                    //заполняю данные для ответа
                    wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                    
                    List<StatusCallbackIResourceLocation> listStatusCallback = new List<StatusCallbackIResourceLocation>();
                    foreach (var VARIABLE in dataCallback.Result)
                    {
                        foreach (var VARIABLE2 in VARIABLE.AllLocations)
                        {
                            StatusCallbackIResourceLocation statusCallback = new StatusCallbackIResourceLocation(StatusCallBackServer.Error, VARIABLE2);
                            listStatusCallback.Add(statusCallback);
                        }
                    }

                    StorageStatusCallbackIResourceLocation statusAll = new StorageStatusCallbackIResourceLocation(listStatusCallback, TypeStorageStatusCallbackIResourceLocator.AllError);

                    wrapperCallbackData.Data.GetData = statusAll;
                    wrapperCallbackData.Data.IsGetDataCompleted = true;
                    wrapperCallbackData.Data.Invoke();
                    
                    _idCallback.Remove(wrapperCallbackData.DataGet.IdMassage);
                    
                    return;
                }
                
            }
        }

        //возр. обертку с callback, когда данные будут готовы(и с неё же получ. данные)
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
