using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Реализация сохранения статуса мута, через отслеживания изменения статуса мута, у триггера. Но у этого триггера должен быть публичный метод Set(с вызовом event на обновления статуса)
/// Нужно, это что бы, когда выгружу данные из превсов, установить последнее состояние триггера
/// (Нужен, если статус мута сохраняеться не у конкретного элемента, а к примеру у группы элементов, и за эту группу отвечает 1 триггер)
/// </summary>
public class CS_SaveStatusMuteTrigger : MonoBehaviour
{
    /// <summary>
    /// Тут нужна реализация триггера с публичным методом Set, при этом в этом методе должен срабатывать event на обновления статуса
    /// </summary>
    [SerializeField] 
    private CS_TriggerStatusMuteTogglePublicSet _trigger;

    [SerializeField] 
    private string _pathSaveData;
    
    private void Start()
    {
        if (_trigger.IsInit == false)
        {
            _trigger.OnInit += OnInitTrigger;
            return;
        }
        
        InitTrigger();
    }

    private void OnInitTrigger()
    {
        _trigger.OnInit -= OnInitTrigger;
        InitTrigger();
    }

    private void InitTrigger()
    {
        _trigger.SetStatusMute(GetStatus());

        _trigger.OnUpdateMuteData += OnUpdateMuteData;
    }
    
    private void OnUpdateMuteData()
    {
        if (_trigger.GetStatusMute() == true) 
        {
            PlayerPrefs.SetString(_pathSaveData, "1");
        }


        if (_trigger.GetStatusMute() == false) 
        {
            PlayerPrefs.SetString(_pathSaveData, "0");
        }
        
        PlayerPrefs.Save();
    }

    private bool GetStatus()
    {
        var status = PlayerPrefs.GetString(_pathSaveData);

        if (status ==  "1")
        {
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        _trigger.OnUpdateMuteData -= OnUpdateMuteData;
    }
}
