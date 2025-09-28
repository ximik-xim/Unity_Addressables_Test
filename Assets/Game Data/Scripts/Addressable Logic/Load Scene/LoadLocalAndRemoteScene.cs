using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Random = UnityEngine.Random;

/// <summary>
/// Отвечает за загрузку сцен
/// </summary>
public class LoadLocalAndRemoteScene : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    /// <summary>
    /// Т.К key являеться object и не сериализ. По этому ключ сериализ. отдельно, а ост. данные сразу в нем сериализую
    /// </summary>
    [SerializeField]
    private DataSceneLoadAddressable _sceneLoadSettings;
    
    [SerializeField] 
    private AbsCallbackGetSceneAddressables _getSceneAddressables;
    
    [SerializeField] 
    private AssetReference _keyNameSceneRemote;
    
    [SerializeField] 
    private AssetReference _keyNameSceneLocal;

    [SerializeField]
    private StorageTypeLog _storageTypeLog;

    public event Action<AbsKeyData<KeyTaskLoaderTypeLog, string>> OnAddLogData;

    private CallbackRequestDataWrapperT<SceneInstance> _wrapperCallbackData;

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
        if (_getSceneAddressables.IsInit == false)
        {
            _getSceneAddressables.OnInit += OnInitGetSceneAddressables;
        }
        
        CheckInit();
    }
    
    private void OnInitGetSceneAddressables()
    {
        if (_getSceneAddressables.IsInit == true)
        {
            _getSceneAddressables.OnInit -= OnInitGetSceneAddressables;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_getSceneAddressables.IsInit == true)
        {
            _isInit = true;
            OnInit?.Invoke();
            
            _isBlock = false;
            OnUpdateStatusBlock?.Invoke();
        }
    }
    
    
    public GetServerRequestData<SceneInstance> StartLoadScene()
    {
        if (_isBlock == false)
        {
            _isBlock = true;
            OnUpdateStatusBlock?.Invoke();
        
            int id = GetUniqueId();
        
            _wrapperCallbackData = new CallbackRequestDataWrapperT<SceneInstance>(id);
            _idCallback.Add(id);
      

        
            DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Запуск загрузку начальной сцены");

            StartLoadRemote();

            return _wrapperCallbackData.DataGet;
        }

        return null;
    }

    private void StartLoadRemote()
    {
        //настройки загрузки сцены
        _sceneLoadSettings = new DataSceneLoadAddressable(_keyNameSceneRemote, _sceneLoadSettings.LoadMode, _sceneLoadSettings.ActivateOnLoad, _sceneLoadSettings.Priority, _sceneLoadSettings.ReleaseMode);
        var dataCallback = _getSceneAddressables.GetData(_sceneLoadSettings);
        
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
                DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Ошибка при загрузки Remote сцены");
                DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Начинаю загрузку локальной сцены");

                StartLoadLocal();
            }
            
            
        }
    }
    
    private void StartLoadLocal()
    {
        //настройки загрузки сцены
        _sceneLoadSettings = new DataSceneLoadAddressable(_keyNameSceneLocal, _sceneLoadSettings.LoadMode, _sceneLoadSettings.ActivateOnLoad, _sceneLoadSettings.Priority, _sceneLoadSettings.ReleaseMode);
        var dataCallback = _getSceneAddressables.GetData(_sceneLoadSettings);
        
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
