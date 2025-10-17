using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Random = UnityEngine.Random;

/// <summary>
/// Отвечает за загрузку сцен
/// Сначало Remote, затем если не удалось, то Local сцены
/// </summary>
public class LoadLocalAndRemoteScene : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private LoadTargetSceneAddressables _loadTargetSceneAddressables;
    
    [SerializeField] 
    private AssetReference _keyNameSceneRemote;
    
    [SerializeField] 
    private AssetReference _keyNameSceneLocal;

    [SerializeField]
    private StorageTypeLog _storageTypeLog;

    public event Action<AbsKeyData<KeyTaskLoaderTypeLog, string>> OnAddLogData;

    private CallbackRequestDataWrapperT<AsyncOperationHandle<SceneInstance>> _wrapperCallbackData;
    
    public bool IsBlock => _isBlock;
    private bool _isBlock = true;
    public event Action OnUpdateStatusBlock;
    
    /// <summary>
    /// Список Id callback, которые сейчас в ожидании
    /// (сериализован просто для удобного отслеживания в инспекторе)
    /// </summary>
    [SerializeField] 
    private List<int> _idCallback = new List<int>();
    
    private void Awake()
    {
        if (_loadTargetSceneAddressables.IsInit == false)
        {
            _loadTargetSceneAddressables.OnInit += OnInitGetSceneAddressables;
        }
        
        CheckInit();
    }
    
    private void OnInitGetSceneAddressables()
    {
        if (_loadTargetSceneAddressables.IsInit == true)
        {
            _loadTargetSceneAddressables.OnInit -= OnInitGetSceneAddressables;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_loadTargetSceneAddressables.IsInit == true)
        {
            _isInit = true;
            OnInit?.Invoke();
            
            _isBlock = false;
            OnUpdateStatusBlock?.Invoke();
        }
    }
    
    
    public GetServerRequestData<AsyncOperationHandle<SceneInstance>> StartLoadScene()
    {
        if (_isBlock == false)
        {
            _isBlock = true;
            OnUpdateStatusBlock?.Invoke();
        
            int id = GetUniqueId();
        
            _wrapperCallbackData = new CallbackRequestDataWrapperT<AsyncOperationHandle<SceneInstance>>(id);
            _idCallback.Add(id);
        
            DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Запуск загрузку начальной сцены");

            StartLoadRemote();

            return _wrapperCallbackData.DataGet;
        }

        return null;
    }

    private void StartLoadRemote()
    {
        var dataCallback = _loadTargetSceneAddressables.StartLoadScene(_keyNameSceneRemote);
        
        if (dataCallback.IsGetDataCompleted == true)
        {
            CompletedCallback();
        }
        else
        {
            dataCallback.OnGetDataCompleted -= OnCompletedCallback;
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
            if (dataCallback.StatusServer == StatusCallBackServer.Ok)
            {
                DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Загрузка Remote сцены успешна");
                
                
                _wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Ok;
                _wrapperCallbackData.Data.GetData = dataCallback.GetData;

                _wrapperCallbackData.Data.IsGetDataCompleted = true;
                _wrapperCallbackData.Data.Invoke();
                
                _idCallback.Remove(_wrapperCallbackData.Data.IdMassage);

                _isBlock = false;
                OnUpdateStatusBlock?.Invoke();
            }
            else
            {
                if (dataCallback.GetData.IsValid() == true) 
                {
                    //Addressables.Release(_handle);
                    Addressables.UnloadSceneAsync(dataCallback.GetData);
                }
                
                DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Ошибка при загрузки Remote сцены");
                DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Начинаю загрузку локальной сцены");

                StartLoadLocal();
            }
            
            
        }
    }
    
    private void StartLoadLocal()
    {
        var dataCallback = _loadTargetSceneAddressables.StartLoadScene(_keyNameSceneLocal);
        
        if (dataCallback.IsGetDataCompleted == true)
        {
            CompletedCallback();
        }
        else
        {
            dataCallback.OnGetDataCompleted -= OnCompletedCallback;
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
            if (dataCallback.StatusServer == StatusCallBackServer.Ok)
            {
                DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Загрузка Local сцены успешна");
                
                _wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Ok;
                _wrapperCallbackData.Data.GetData = dataCallback.GetData;

                _wrapperCallbackData.Data.IsGetDataCompleted = true;
                _wrapperCallbackData.Data.Invoke();

                _idCallback.Remove(_wrapperCallbackData.Data.IdMassage);
                
                _isBlock = false;
                OnUpdateStatusBlock?.Invoke();
            }
            else
            {
                DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Ошибка при загрузки Local сцены");
                
                _wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                _wrapperCallbackData.Data.GetData = dataCallback.GetData;

                _wrapperCallbackData.Data.IsGetDataCompleted = true;
                _wrapperCallbackData.Data.Invoke();

                _idCallback.Remove(_wrapperCallbackData.Data.IdMassage);

                if (dataCallback.GetData.IsValid() == true) 
                {
                    //Addressables.Release(_handle);
                    Addressables.UnloadSceneAsync(dataCallback.GetData);
                }
                
                _isBlock = false;
                OnUpdateStatusBlock?.Invoke();
            }
        }
    }
    
    private void DebugLog(KeyTaskLoaderTypeLog keyLog, string text)
    {
        var logData = new AbsKeyData<KeyTaskLoaderTypeLog, string>(keyLog, text);
        OnAddLogData?.Invoke(logData);
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
