using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosOpenPanelTaskUI : AbsClosOpenPanelTask
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
