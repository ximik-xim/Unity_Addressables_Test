using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseAndOpenUIGamePanelInfoGetPanel : MonoBehaviour
{
    [SerializeField] 
    private GetUIGamePanelInfoStorageInStorage _getUIGamePanel;
    private UIGamePanelInfo _uiGamePanelInfo;

    [SerializeField] 
    private Button _button;

    [SerializeField] 
    private bool _isIgnorTask;
    
    [SerializeField]
    private TypeActionCloseAndOpenUIGamePanelInfoGetPanel _typeAction;
    
    private void Awake()
    {
        if (_getUIGamePanel.IsInit == false)
        {
            _getUIGamePanel.OnInit += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _getUIGamePanel.OnInit -= OnInit;
        Init();
    }

    private void Init()
    {
        _uiGamePanelInfo = _getUIGamePanel.GetPanel();
        
        _button.onClick.AddListener(ButtonClick);  
    }
    
    private void ButtonClick()
    {
        if (_typeAction == TypeActionCloseAndOpenUIGamePanelInfoGetPanel.Open)
        {
            if (_isIgnorTask == true)
            {
                _uiGamePanelInfo.OpenIgnoreTask();    
                return;    
            }
        
            _uiGamePanelInfo.Open();
        }
        
        if (_typeAction == TypeActionCloseAndOpenUIGamePanelInfoGetPanel.Close)
        {
            if (_isIgnorTask == true)
            {
                _uiGamePanelInfo.CloseIgnoreTask();    
                return;    
            }
        
            _uiGamePanelInfo.Close();
        }
    }


    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}

public enum TypeActionCloseAndOpenUIGamePanelInfoGetPanel
{
    Open,
    Close
}
