using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Отвечает за загрузку и переход на сцену через Task 
/// Т.к сразу после загр сцены, происходит переход на сцену, то тут костыль в DontDestroy,
/// который должен отработать и сказать, что Task закончила выполнение
/// Если все же нужно что бы Task прожила до перехода на след. сцену и потом сама себя уничтожила,
/// то есть _isDestroyTaskGmComplited, если его включить, то Task сама себя уничтожит после загр. сцены
/// </summary>
public class TaskLoadSceneAddressable : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private AbsLoadTargetSceneKey _sceneLoad;

    //идет ли загр. сцены
    private bool _isLoadScene = false;

    [SerializeField]
    //Будет ли уничтожина Task при переход. на след сцену(нежно в случ, если Task будет помеч как DontDestoy)
    private bool _isDestroyTaskGmComplited = false;
    
    private void Awake()
    {
        if (_sceneLoad.IsInit == false)
        {
            _sceneLoad.OnInit += OnInitSceneLoad;
        }
        
        CheckInit();
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
        if (_sceneLoad.IsInit == true)
        {
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

    protected override void StartLogic()
    {
        UpdatePercentage(50f);  
        
        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Начинаю загрузку сцены");
        
        _isLoadScene = true;
        
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
            
            //запуск сцены вручную
            //dataCallback.GetData.ActivateAsync();
            
            _isLoadScene = false;

            if (_isDestroyTaskGmComplited == true)
            {
                Destroy(this.gameObject);
            }
        }
    }

    protected override void BreakTask()
    {
    
    }

    protected override void ResetStatusTask()
    {
    
    }
    
    protected void OnDestroy()
    {
        //Если идет загр. сцены и сработал DontDestroy, значит сцена была удачно загружена
        if (_isLoadScene == true)
        {
            UpdatePercentage(100f);  
            UpdateStatus(TypeStatusTaskLoad.Comlite);

            _isLoadScene = false;
        }
        
        DestroyLogic();
    }
}
