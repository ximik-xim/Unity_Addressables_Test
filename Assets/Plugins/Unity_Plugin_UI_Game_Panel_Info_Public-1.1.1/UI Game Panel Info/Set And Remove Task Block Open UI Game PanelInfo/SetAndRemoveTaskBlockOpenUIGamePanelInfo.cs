using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SetAndRemoveTaskBlockOpenUIGamePanelInfo : MonoBehaviour
{
    //Тут путь по которому получу panel
    [SerializeField]
    private GetDKOPatch _patchStorageGamePanelInfo;

    [SerializeField] 
    private GetDataSO_KeyUIGamePanelInfo _keyPanel;
    private UIGamePanelInfo _panelInfo;
    
    [SerializeField] 
    private GetDataSO_TSG_KeyTaskData _keyBlockPanelOpen;
    
    [SerializeField] 
    private string _textBlockOpen;
    
    [SerializeField] 
    private bool _removeTaskBlockOpenOnDestroy = true;
    
    [SerializeField] 
    private GetDataSO_TSG_KeyTaskData _keyBlockPanelClose;

    [SerializeField] 
    private string _textBlockClose;

    [SerializeField] 
    private bool _removeTaskBlockCloseOnDestroy = true;
    
    
    private bool _isInit = false;
    public bool IsInit => _isInit;
    public event Action OnInit;

    private void Awake()
    {
        if (_patchStorageGamePanelInfo.Init == false)
        {
            _patchStorageGamePanelInfo.OnInit += OnInitStoragePanel;
            return;
        }

        GetDataDKO();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageGamePanelInfo.OnInit -= OnInitStoragePanel;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
        UIGamePanelInfoStorage taskInfo = DKOData.Data;
        _panelInfo = taskInfo.GetPanel(_keyPanel.GetData());

        _isInit = true;
        OnInit?.Invoke();
    }

    public void SetTaskBlockOpen()
    {
        if (_panelInfo.GetTaskDataOpen().IsKeyTask(_keyBlockPanelOpen.GetData()) == false)
        {
            _panelInfo.GetTaskDataOpen().AddTask(_keyBlockPanelOpen.GetData(), _textBlockOpen);
        }
    }

    public void RemoveTaskBlockOpen()
    {
        if (_panelInfo.GetTaskDataOpen().IsKeyTask(_keyBlockPanelOpen.GetData()) == true)
        {
            _panelInfo.GetTaskDataOpen().RemoveTask(_keyBlockPanelOpen.GetData());
        }
    }

    public void SetTaskBlockClose()
    {
        if (_panelInfo.GetTaskDataOpen().IsKeyTask(_keyBlockPanelClose.GetData()) == false)
        {
            _panelInfo.GetTaskDataOpen().AddTask(_keyBlockPanelClose.GetData(), _textBlockClose);
        }
    }

    public void RemoveTaskBlockClose()
    {
        if (_panelInfo.GetTaskDataOpen().IsKeyTask(_keyBlockPanelClose.GetData()) == true)
        {
            _panelInfo.GetTaskDataOpen().RemoveTask(_keyBlockPanelClose.GetData());
        }
    }
    
    private void OnDestroy()
    {
        if (_removeTaskBlockOpenOnDestroy == true)
        {
            RemoveTaskBlockOpen();
        }
        
        if (_removeTaskBlockCloseOnDestroy == true)
        {
            RemoveTaskBlockOpen();
        }
    }
}
