using UnityEngine;

/// <summary>
/// Триггера на включение панели логера, когда будет начата кнопка на UI Task
/// 
/// (перенесет себя с Task на Task UI, что бы работать, даже если Task будет уничтожена
/// (к примеру при переходе между сцен), а Task UI может остаться)
/// </summary>
public class LogicTriggerButtonOpenPanelLogger : MonoBehaviour
{
    private TriggerClickOpenLogger _triggerClickOpenLogger;
    private LoggerPanelUI _loggerPanelUI;
    
    public void SetData(TriggerClickOpenLogger triggerClickOpenLogger, LoggerPanelUI loggerPanelUI)
    {
        _triggerClickOpenLogger = triggerClickOpenLogger;
        _loggerPanelUI = loggerPanelUI;
        
        _triggerClickOpenLogger.OnClick += OnButtonClick;
        
        //Переносим скрипт на GM логгера
        this.gameObject.transform.parent = loggerPanelUI.transform;
    }
    
    private void OnButtonClick()
    {
        _loggerPanelUI.Open();
    }
    
    private void OnDestroy()
    {
        if (_triggerClickOpenLogger != null) 
        {
            _triggerClickOpenLogger.OnClick -= OnButtonClick;   
        }
    }
}
