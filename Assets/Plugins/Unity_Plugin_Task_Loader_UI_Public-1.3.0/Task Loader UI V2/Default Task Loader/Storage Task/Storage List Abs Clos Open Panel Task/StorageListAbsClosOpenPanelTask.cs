using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен что бы получить абстракцию от панелей Task, для их включения и отключения
/// (получать панель буду по DKO котор наход у Task Panel UI, что бы знать к какой Task Panel UI обращаюсь) 
/// </summary>
public class StorageListAbsClosOpenPanelTask : MonoBehaviour
{
    private Dictionary<DKOKeyAndTargetAction,AbsClosOpenPanelTask> _taskOpenCloseUI = new Dictionary<DKOKeyAndTargetAction, AbsClosOpenPanelTask>();

    public void AddData(DKOKeyAndTargetAction key, AbsClosOpenPanelTask data)
    {
        _taskOpenCloseUI.Add(key, data);
    }

    public void RemoveData(DKOKeyAndTargetAction key)
    {
        _taskOpenCloseUI.Remove(key);
    }
    
    public AbsClosOpenPanelTask GetTaskPanel(DKOKeyAndTargetAction objectDKO)
    {
        return _taskOpenCloseUI[objectDKO];
    }

}
