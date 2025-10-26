using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Позволяет включать и отключать панель Task
/// (по идеи будет находиться на логике Task а не на панели Task UI, по этому надо это учитывать)
/// </summary>
public class CloseOpenPanelTaskUI : AbsClosOpenPanelTask
{
    [SerializeField] 
    private SpawnerTaskPanelUI _spawnTaskUI;


    public override void OpenPanel()
    {
        _spawnTaskUI.GetTaskUI().OpenPanel();
    }

    public override void ClosePanel()
    {
        _spawnTaskUI.GetTaskUI().ClosePanel();
    }
}
