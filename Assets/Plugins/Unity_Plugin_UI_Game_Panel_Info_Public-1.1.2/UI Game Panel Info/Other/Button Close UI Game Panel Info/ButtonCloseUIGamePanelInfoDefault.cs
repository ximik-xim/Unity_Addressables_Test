using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseUIGamePanelInfoDefault : MonoBehaviour
{
    [SerializeField] 
    private bool _isIgnorTask;
    
    [SerializeField] 
    private Button _buttonClose;

    [SerializeField]
    private UIGamePanelInfo _panelInfo;
    
    private void Awake()
    {
        _buttonClose.onClick.AddListener(ButtonClickClose);  
    }
    
    private void ButtonClickClose()
    {
        if (_isIgnorTask == true)
        {
            _panelInfo.CloseIgnoreTask();    
            return;    
        }
        
        _panelInfo.Close();
    }


    private void OnDestroy()
    {
        _buttonClose.onClick.RemoveListener(ButtonClickClose);
    }
}
