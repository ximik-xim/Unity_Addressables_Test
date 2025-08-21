using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Это обертка нужна, что бы сделать переотправку запроса несколько раз(в случ. ошибки)
/// </summary>
public class GetDataAddressablesErrorContinue : AbsCallbackGetDataAddressables
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
    
    [SerializeField]
    private AbsCallbackGetDataAddressables _absGetDataAddressables;

    [SerializeField]
    private LogicErrorCallbackRequestAddressables _errorLogic;

    private void Awake()
    {
        if (_errorLogic.IsInit == false)
        {
            _errorLogic.OnInit += OnInitErrorLogic;
        }
       
        if (_absGetDataAddressables.IsInit == false)
        {
            _absGetDataAddressables.OnInit += OnInitGetData;
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
        _absGetDataAddressables.OnInit -= OnInitGetData;
        CheckInit();
    }

    private void CheckInit()
    {
        if (_isInit == false)
        {
            if (_errorLogic.IsInit == true && _absGetDataAddressables.IsInit == true)
            {
                _isInit = true;
                OnInit?.Invoke();
            }
        }
    }
    public override GetServerRequestData<T> GetData<T>(object data)
    {
        //запрашиваю данные
        var dataCallback = _absGetDataAddressables.GetData<T>(data);
        //делаю обертку т.к могу несколько раз делать запросы на данные, а верну лиш 1 итог. результат 
        CallbackRequestDataAddressablesWrapper<T> wrapperCallbackData = new CallbackRequestDataAddressablesWrapper<T>(dataCallback.IdMassage);

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
                    //заного отпр. запрос, и по новой 
                    dataCallback = _absGetDataAddressables.GetData<T>(data);
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
