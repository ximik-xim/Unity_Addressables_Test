using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кнопки переключения типа логов
/// Хранит в себе указ тип. лога,
/// и передает его в event при нажатии на эту кнопку
/// </summary>
public class ButtonTypeLog : MonoBehaviour
{
    public event Action<KeyTaskLoaderTypeLog> OnButtonClick;
    
    [SerializeField] 
    private RawImage _image;

    [SerializeField] 
    private GetDataSO_KeyTaskLoaderTypeLog _getKey;
    private KeyTaskLoaderTypeLog _keyTypeLog;

    [SerializeField] 
    private GameObject TaskLoggerPanel;

    [SerializeField] 
    private Button _button;
    private void Awake()
    {
        _keyTypeLog = _getKey.GetData();
        _button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        OnButtonClick?.Invoke(_keyTypeLog);
    }
    
    
    public void Open()
    {
        TaskLoggerPanel.SetActive(true);
    }
    
    public void Close()
    {
        TaskLoggerPanel.SetActive(false);
    }

}
