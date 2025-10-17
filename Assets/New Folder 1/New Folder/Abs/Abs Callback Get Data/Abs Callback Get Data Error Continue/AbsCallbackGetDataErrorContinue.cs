using System;
using UnityEngine;

/// <summary>
/// Это обертка нужна, что бы сделать переотправку запроса несколько раз(в случ. ошибки)
/// </summary>
[System.Serializable]
public class AbsCallbackGetDataErrorContinue<AbsCallbackGet, GetType, ArgData>
    where AbsCallbackGet : AbsCallbackGetDataT<GetType, ArgData> 
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;

    [SerializeField]
    private AbsCallbackGet _absGetData;

    [SerializeField]
    private LogicErrorCallbackRequest _errorLogic;

    public void StartInit()
    {
        if (_errorLogic.IsInit == false)
        {
            _errorLogic.OnInit += OnInitErrorLogic;
        }
       
        if (_absGetData.IsInit == false)
        {
            _absGetData.OnInit += OnInitGetData;
        }

        CheckInit();
    }

    private void OnInitErrorLogic()
    {
        _errorLogic.OnInit -= OnInitErrorLogic;
        CheckInit();
    }
    
    private void OnInitGetData()
    {
        _absGetData.OnInit -= OnInitGetData;
        CheckInit();
    }

    private void CheckInit()
    {
        if (_isInit == false)
        {
            if (_errorLogic.IsInit == true && _absGetData.IsInit == true)
            {
                _isInit = true;
                OnInit?.Invoke();
            }
        }
    }
    public GetServerRequestData<GetType> GetData(ArgData data)
    {
        Debug.Log("Запрос на загр. обьекта был отправлен");
        //Тут именно скопировать нужно
        ArgData copiedData = data;
        
        //запрашиваю данные
        var dataCallback = _absGetData.GetData(copiedData);
        //делаю обертку т.к могу несколько раз делать запросы на данные, а верну лиш 1 итог. результат 
        CallbackRequestDataWrapperT<GetType> wrapperCallbackData = new CallbackRequestDataWrapperT<GetType>(dataCallback.IdMassage);

        //проверяю готовы ли данные 
        if (dataCallback.IsGetDataCompleted == true)
        {
            //да готовы, начинаю обработку
            CompletedCallback();
        }
        else
        {
            //не, неготовы, начинаю ожидание, пока прийдут
            dataCallback.OnGetDataCompleted += OnCompletedCallback;
        }
        
        void OnCompletedCallback()
        {
            //Если данные пришли
            if (dataCallback.IsGetDataCompleted == true)
            {
                dataCallback.OnGetDataCompleted -= OnCompletedCallback;
                //начинаю обработку данных
                CompletedCallback();    
            }
        }

        void CompletedCallback()
        {
            //Если успешно получил данные
            if (dataCallback.StatusServer == StatusCallBackServer.Ok)
            {
                Debug.Log("Запрос на загр. обьекта успешен");
                //очищаю список ошибок
                _errorLogic.OnRemoveAllError();
                
                //заполняю данные для ответа
                wrapperCallbackData.Data.StatusServer = dataCallback.StatusServer;
                wrapperCallbackData.Data.GetData = dataCallback.GetData;

                wrapperCallbackData.Data.IsGetDataCompleted = true;
                wrapperCallbackData.Data.Invoke();
                return;
            }
            else
            {
                //добавляю ошибку
                _errorLogic.OnAddError();
                
                //Проверяю, могу ли еще раз отпр. запрос
                if (_errorLogic.IsContinue == true) 
                {
                    Debug.Log("Запрос на загр. обьекта ошибка. Переотправка");
                    
                    //заного отпр. запрос, и по новой 
                    dataCallback = _absGetData.GetData(copiedData);
                    if (dataCallback.IsGetDataCompleted == true)
                    {
                        CompletedCallback();
                    }
                    else
                    {
                        dataCallback.OnGetDataCompleted -= OnCompletedCallback;
                        dataCallback.OnGetDataCompleted += OnCompletedCallback;
                    }
                    
                    return;
                }
                else
                {
                    Debug.Log("Запрос на загр. обьекта ошибка. Попытки кончились. Возр. ERROR");
                    //если попытки достучаться до сервера закончились, то отпр. все как есть(ошибку)
                    
                    //очищаю список ошибок
                    _errorLogic.OnRemoveAllError();
                    
                    //заполняю данные для ответа
                    wrapperCallbackData.Data.StatusServer = dataCallback.StatusServer;
                    wrapperCallbackData.Data.GetData = dataCallback.GetData;

                    wrapperCallbackData.Data.IsGetDataCompleted = true;
                    wrapperCallbackData.Data.Invoke();
                    
                    return;
                }
                
            }
        }

        //возр. обертку с callback, когда данные будут готовы(и с неё же получ. данные)
        return wrapperCallbackData.DataGet;
    }
}