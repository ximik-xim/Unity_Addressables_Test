using UnityEngine;

/// <summary>
/// Отвечает за загрузку и переход на сцену через Task
/// </summary>
public class TaskLoadScene : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private AbsRequestStartLoadScene _sceneLoad;

    private GetServerRequestData<RequestStartLoadSceneData> callback;
    
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

        this.gameObject.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
       


        callback = _sceneLoad.StartLoadScene();


        if (callback.IsGetDataCompleted == false)
        {
            callback.OnGetDataCompleted += OnGetDataCompleted;
        }
        else
        {
            GetDataCompleted();
        }

        void OnGetDataCompleted()
        {
            if (callback.IsGetDataCompleted == true)
            {
                callback.OnGetDataCompleted -= OnGetDataCompleted;
                GetDataCompleted();
            }
        }

        void GetDataCompleted()
        {
            UpdatePercentage(100f);
            UpdateStatus(TypeStatusTaskLoad.Comlite);

            Destroy(this.gameObject);
        }
    }



    protected override void BreakTask()
    {
    
    }

    protected override void ResetStatusTask()
    {
    
    }
    
    
}
