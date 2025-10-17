using UnityEngine;

/// <summary>
/// Отвечает за запуск логики нумерации сцен
/// </summary>
public class TaskStartNumberScene : AbsTaskLoggerLoaderDataMono
{
    [Header("---------")]
    [SerializeField]
    private LogicStartNumberScene _logicStartNumberScene;
    
    private void Awake()
    {
        if (_logicStartNumberScene.IsInit == false)
        {
            _logicStartNumberScene.OnInit += OnInitLogicStartNumberScene;
        }
        
        CheckInit();
    }
    
    private void OnInitLogicStartNumberScene()
    {
        if (_logicStartNumberScene.IsInit == true)
        {
            _logicStartNumberScene.OnInit -= OnInitLogicStartNumberScene;
            CheckInit();
        }
        
    }
    
    
    private void CheckInit()
    {
        if (_logicStartNumberScene.IsInit == true)   
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
        UpdateStatus(TypeStatusTaskLoad.Start);
        UpdateStatus(TypeStatusTaskLoad.Load);

        _storageLog.DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Запуск нумерацию сцен");
        
        _logicStartNumberScene.StartNumberScene();
        
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
        DestroyLogic();
    }
}
