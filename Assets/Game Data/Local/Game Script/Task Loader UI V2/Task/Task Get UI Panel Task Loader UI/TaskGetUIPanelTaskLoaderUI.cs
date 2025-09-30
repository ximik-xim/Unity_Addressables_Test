using UnityEngine;

public class TaskGetUIPanelTaskLoaderUI : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private GetUIPanelTaskLoaderUI _getUIPanelTaskLoaderUI;
    
    private void Awake()
    {
        if (_getUIPanelTaskLoaderUI.IsInit == false)
        {
            _getUIPanelTaskLoaderUI.OnInit += OnInitCopyKeySceneMbsDko;
        }

       
        CheckInit();
    }
    
    private void OnInitCopyKeySceneMbsDko()
    {
        if (_getUIPanelTaskLoaderUI.IsInit == true)
        {
            _getUIPanelTaskLoaderUI.OnInit -= OnInitCopyKeySceneMbsDko;
            CheckInit();
        }
        
    }
    
    
    private void CheckInit()
    {
        if (_getUIPanelTaskLoaderUI.IsInit == true)   
        {
            _getUIPanelTaskLoaderUI.OnAddLogData += OnAddLogDataTaskLoadScene;
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
        
        _getUIPanelTaskLoaderUI.StartLogic();
        
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
        _getUIPanelTaskLoaderUI.OnAddLogData -= OnAddLogDataTaskLoadScene;
        DestroyLogic();
    }

}
