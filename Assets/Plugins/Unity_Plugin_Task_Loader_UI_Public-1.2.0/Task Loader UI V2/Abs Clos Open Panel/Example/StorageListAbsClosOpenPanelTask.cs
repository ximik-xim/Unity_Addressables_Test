using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен что бы получить абстракцию от панелей Task, для их включения и отключения 
/// </summary>
public class StorageListAbsClosOpenPanelTask : MonoBehaviour
{
    // ага тут могу отключать и влкючать панели
    //     
    //     Наверное тогда нужно хран через котор смогу получ GM Task
    //     и отдельное хран для GM Логера
    //     
    //     и потом по ним проходить и удалять
    //
    // а стоп, а как наход. соответсвтие....
    //
    // Надо подум.
    //     
    //     
    //     
    //     ПРИДУМАЛ. У Task есть же DKO
    //
    //
    //
    //     Дак вот можно хрнить список DKO Task (и если Task хранит у себя ссылку или еще что на Panel, то получ и эту panel и уничтожить все)
    //         ИЛИ, что будет более грамотно. Это на Task сделать отдельный скрипт отвеч за уничтожение Task как GM (и если она имеет логеер, то пусть сам запускает и уничтожение его)
    //             
    //             (только нужно сохранять DKO не логики запуск Task. А самой UI Task)
    //
    //     и удал Task из Storage Task, не означет, что было удал из UI !!! (это важно)
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
