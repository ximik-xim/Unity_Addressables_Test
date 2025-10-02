using UnityEngine;

/// <summary>
/// Логика триггера на уничтожение GM логера, когда будет уничтожена UI Task
/// </summary>
public class LogicTriggerDestroyGmPanelLogger : MonoBehaviour
{
    private LogicDestroyTaskUI _destroyTaskUI;
    private LoggerPanelUI _loggerPanelUI;
    
    public void SetData(LogicDestroyTaskUI destroyTaskUI, LoggerPanelUI loggerPanelUI)
    {
        _destroyTaskUI = destroyTaskUI;
        _loggerPanelUI = loggerPanelUI;
        
        _destroyTaskUI.OnStartDestroy += StartDestroyLogger;
        
        //Переносим скрипт на GM логгера
        this.gameObject.transform.parent = loggerPanelUI.transform;
    }
    
    private void StartDestroyLogger()
    {
        _loggerPanelUI.StartDestroy();
    }
    
    private void OnDestroy()
    {
        if (_destroyTaskUI != null) 
        {
            _destroyTaskUI.OnStartDestroy -= StartDestroyLogger;    
        }
    }
}
