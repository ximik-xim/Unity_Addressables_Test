using System;
using UnityEngine;

/// <summary>
/// Отвечает за запуск загрузки обновлений
/// и за запуск загрузки сцен
/// </summary>
public class TaskLoggerUpdateAllObjectAndLoadScene : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private AbsCheckAndDownloadUpdateObject _checkAndDownloadUpdateObjectAll;

    [SerializeField]
    private LoadLocalAndRemoteScene _sceneLoad;
    
    private void Awake()
    {
        if (_checkAndDownloadUpdateObjectAll.IsInit == false)
        {
            _checkAndDownloadUpdateObjectAll.OnInit += OnInitCheckAndDownloadUpdateObjectAll;
        }

        if (_sceneLoad.IsInit == false)
        {
            _sceneLoad.OnInit += OnInitSceneLoad;
        }
        
        CheckInit();
    }
    
    private void OnInitCheckAndDownloadUpdateObjectAll()
    {
        if (_checkAndDownloadUpdateObjectAll.IsInit == true)
        {
            _checkAndDownloadUpdateObjectAll.OnInit -= OnInitCheckAndDownloadUpdateObjectAll;
            CheckInit();
        }
        
    }
    
    private void OnInitSceneLoad()
    {
        if (_sceneLoad.IsInit == true)
        {
            _sceneLoad.OnInit -= OnInitSceneLoad;
            CheckInit();
        }
        
    }
    
    private void CheckInit()
    {
        if (_checkAndDownloadUpdateObjectAll.IsInit == true && _sceneLoad.IsInit == true)  
        {
            _sceneLoad.OnAddLogData += OnAddLogDataTaskLoadScene;

            Init();
        }
    }
    
    public override TaskLoaderData GetTaskInfo()
    {
        if (_taskData == null) 
        {
            InitTask();
        }
        
        //тут убир. авто иниц.
        
        return _taskData;
    }

    private void OnAddLogDataTaskLoadScene(AbsKeyData<KeyTaskLoaderTypeLog, string> textLog)
    {
        _storageLog.DebugLog(textLog.Key, textLog.Data);
    }
    
    protected override void StartLogic()
    {
        UpdateStatus(TypeStatusTaskLoad.Start);
        UpdateStatus(TypeStatusTaskLoad.Load);
        
        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Запуск проверки и загрузки обновлений");
        
        var dataCallback = _checkAndDownloadUpdateObjectAll.StartCheckUpdateCatalog();
        
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
                if (dataCallback.GetData != null) 
                {
                    if (dataCallback.GetData.StatusAllCallBack == TypeStorageStatusCallbackIResourceLocator.Ok)
                    {
                        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Логика обновления отработала успешно");
                    }
                    else
                    {
                        _storageLog.DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Для какого то обьекта не удалось загрузить обновление ");
                    }
                }
                else
                {
                    _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Логика обновления отработала успешно. Обновлений НЕТ");
                }
            }
            else
            {
                _storageLog.DebugLog(_storageTypeLog.GetKeyErrorLog(), "- Ошибка при выполнений логики проверки и загрузки обновлений ");
            }
            
            UpdatePercentage(50);  
            
            if (_sceneLoad.IsBlock == false)
            {
                StartLoadScene();
            }
            else
            {
                _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Ожидаю снятия блокировки с логики загрузки сцен");
                
                _sceneLoad.OnUpdateStatusBlock -= OnCheckRemoveBlock;
                _sceneLoad.OnUpdateStatusBlock += OnCheckRemoveBlock;
            }
            
            
        }
    }

    private void OnCheckRemoveBlock()
    {
        if (_sceneLoad.IsBlock == false) 
        {
            _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Снятие блокировки с логики загрузки сцен");
            _sceneLoad.OnUpdateStatusBlock -= OnCheckRemoveBlock;

            StartLoadScene();
        }
        
    }

    private void StartLoadScene()
    {
        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Начинаю загрузку начальной сцены");
        
        var dataCallback = _sceneLoad.StartLoadScene();
        
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
            UpdatePercentage(100f);  
            UpdateStatus(TypeStatusTaskLoad.Comlite);
        }


    }
    

    protected override void BreakTask()
    {
        
    }

    protected override void ResetStatusTask()
    {
        
    }

    private void OnDestroy()
    {
        _sceneLoad.OnAddLogData -= OnAddLogDataTaskLoadScene;
        DestroyLogic();
    }
}
