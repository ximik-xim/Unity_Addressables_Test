using UnityEngine;

/// <summary>
/// Логика триггера на уничтожение GM логера, когда будет уничтожена UI Task
/// 
/// (перенесет себя с Task на Task UI, что бы работать, даже если Task будет уничтожена
/// (к примеру при переходе между сцен), а Task UI может остаться)
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
