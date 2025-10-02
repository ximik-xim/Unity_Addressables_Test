using UnityEngine;

/// <summary>
/// Триггера на включение панели логгера, когда будет начата кнопка на UI Task
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
