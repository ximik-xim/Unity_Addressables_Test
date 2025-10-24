using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// Отвечает за загрузку сцен
/// Сначала Remote через Addressable,
/// затем если не удалось, то Local сцену котора загр. через SceneManager(и эта сцена вшита в игру как запасная)
/// </summary>
public class LoadLocalDefaultAndRemoteAddressablesScene : MonoBehaviour
{ 
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private LoadTargetSceneAddressables _loadTargetSceneAddressables;
    
    [SerializeField] 
    private AssetReference _keyNameSceneRemote;
    
    [SerializeField]
    private LoadTargetScene _loadTargetSceneDefault;
    
    [SerializeField] 
    private GetDataSO_NameSceneAndKeyString _keyNameSceneLocal;

    [SerializeField]
    private StorageTypeLog _storageTypeLog;

    public event Action<AbsKeyData<KeyTaskLoaderTypeLog, string>> OnAddLogData;

    /// <summary>
    /// в даном случае просто заглушка, т.к исп. 2 совершенно разных способа(метода) загрузки сцен
    /// </summary>
    private CallbackRequestDataWrapperT<string> _wrapperCallbackData;
    
    public bool IsBlock => _isBlock;
    private bool _isBlock = true;
    public event Action OnUpdateStatusBlock;
    
    /// <summary>
    /// Список Id callback, которые сейчас в ожидании
    /// (сериализован просто для удобного отслеживания в инспекторе)
    /// </summary>
    [SerializeField] 
    private List<int> _idCallback = new List<int>();

    /// <summary>
    /// Нужно ли автоматически помеч. обьект как Dont Destroy
    /// А после окончания логики просто перемещать на сцену
    /// (нужно в случае если будет использовать лиш 1 раз, а не многоразово)
    /// </summary>
    [SerializeField]
    private bool _isAutoDontDestroyLogic = true;

    /// <summary>
    /// Нужен для тестов, то бы запускать сращу загрузку локальной сцен
    /// </summary>
#if UNITY_EDITOR
    [SerializeField]
    private bool _isStartLoadLocal;
#endif
    
    
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
    
    /// <summary>
    /// string тут просто как заглушка, т.к способы загрузки сцены совсем разные, пока не понятно что возвращать
    /// (в теор. можно было бы возращать Scene, но в случае addressable, я её получу после полной загр. сцены... ну такое)
    /// </summary>
    /// <returns></returns>
    public GetServerRequestData<string> StartLoadScene()
    {
        if (_isBlock == false)
        {
            if (_isAutoDontDestroyLogic == true)
            {
                this.gameObject.transform.parent = null;
                DontDestroyOnLoad(this.gameObject);
            }

            _isBlock = true;
            OnUpdateStatusBlock?.Invoke();
        
            int id = GetUniqueId();
        
            _wrapperCallbackData = new CallbackRequestDataWrapperT<string>(id);
            _idCallback.Add(id);
        
            DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Запуск загрузку начальной сцены");

            
#if UNITY_EDITOR
            if (_isStartLoadLocal == true)
            {
                StartLoadLocal();
                
                return _wrapperCallbackData.DataGet;
            }
#endif
            
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
                _wrapperCallbackData.Data.GetData = "";

                _wrapperCallbackData.Data.IsGetDataCompleted = true;
                _wrapperCallbackData.Data.Invoke();
                
                _idCallback.Remove(_wrapperCallbackData.Data.IdMassage);

                _isBlock = false;
                OnUpdateStatusBlock?.Invoke();
                
                if (_isAutoDontDestroyLogic == true)
                {
                    RemoveDontDestroy();
                }
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
        var dataCallback = _loadTargetSceneDefault.StartLoadScene(_keyNameSceneLocal.GetData().GetNameScene());
        
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
                _wrapperCallbackData.Data.GetData = "";

                _wrapperCallbackData.Data.IsGetDataCompleted = true;
                _wrapperCallbackData.Data.Invoke();

                _idCallback.Remove(_wrapperCallbackData.Data.IdMassage);

                _isBlock = false;
                OnUpdateStatusBlock?.Invoke();
                
                if (_isAutoDontDestroyLogic == true)
                {
                    RemoveDontDestroy();
                }
            }
            else
            {
                DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Ошибка при загрузки Local сцены");
                
                _wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                _wrapperCallbackData.Data.GetData = "";

                _wrapperCallbackData.Data.IsGetDataCompleted = true;
                _wrapperCallbackData.Data.Invoke();

                _idCallback.Remove(_wrapperCallbackData.Data.IdMassage);

                // if (dataCallback.GetData.IsValid() == true) 
                // {
                //     //Addressables.Release(_handle);
                //     Addressables.UnloadSceneAsync(dataCallback.GetData);
                // }
                
                _isBlock = false;
                OnUpdateStatusBlock?.Invoke();
                
                if (_isAutoDontDestroyLogic == true)
                {
                    RemoveDontDestroy();
                }
            }
        }
    }

    private void RemoveDontDestroy()
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
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
