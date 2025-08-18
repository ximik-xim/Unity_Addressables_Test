using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Пример триггера с публичным методом для установки статуса мута
/// </summary>
public class CS_TriggerStatusMuteTogglePublicSet : CS_AbsTriggerStatusMute
{
    [SerializeField] 
    private Toggle _mute;
    
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnUpdateMuteData;
    
    private void Awake()
    {
        _mute.onValueChanged.AddListener(ClickMute);
        OnInit?.Invoke();
    }

    private void ClickMute(bool isMute)
    {
        OnUpdateMuteData?.Invoke();
    }
    
    public void SetStatusMute(bool isMute)
    {
        if (isMute != _mute.isOn)
        {
            _mute.SetIsOnWithoutNotify(isMute);
            
            OnUpdateMuteData?.Invoke();
        }
    }


    public override void SetStatusMuteNotUseEvent(bool isMute)
    {
        _mute.SetIsOnWithoutNotify(isMute);
    }

    public override bool GetStatusMute()
    {
        return _mute.isOn;
    }
    
    private void OnDestroy()
    {
        _mute.onValueChanged.RemoveListener(ClickMute);
    }
}
