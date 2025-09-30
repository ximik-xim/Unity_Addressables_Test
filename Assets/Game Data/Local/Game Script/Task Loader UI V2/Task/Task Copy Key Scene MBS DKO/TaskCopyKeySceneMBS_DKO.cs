using UnityEngine;

public class TaskCopyKeySceneMBS_DKO : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private CopyKeySceneMBS_DKO _copyKeySceneMbsDko;
    
    private void Awake()
    {
        if (_copyKeySceneMbsDko.IsInit == false)
        {
            _copyKeySceneMbsDko.OnInit += OnInitCopyKeySceneMbsDko;
        }
        
        CheckInit();
    }
    
    private void OnInitCopyKeySceneMbsDko()
    {
        if (_copyKeySceneMbsDko.IsInit == true)
        {
            _copyKeySceneMbsDko.OnInit -= OnInitCopyKeySceneMbsDko;
            CheckInit();
        }
        
    }
    
    
    private void CheckInit()
    {
        if (_copyKeySceneMbsDko.IsInit == true)   
        {
            _copyKeySceneMbsDko.OnAddLogData += OnAddLogDataTaskLoadScene;
            Init();
        }
    }
    
    public override TaskLoaderData GetTaskInfo()
    {
        Debug.Log("Get Task");
        
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

        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Запуск копирования ключей");
        
        _copyKeySceneMbsDko.StartLogic();
        
        UpdatePercentage(100f);  
        UpdateStatus(TypeStatusTaskLoad.Comlite);
        
    }

    protected override void BreakTask()
    {

    }

    protected override void ResetStatusTask()
    {

    }

    private void OnDestroy()
    {
        _copyKeySceneMbsDko.OnAddLogData -= OnAddLogDataTaskLoadScene;
        DestroyLogic();
    }
    

}
