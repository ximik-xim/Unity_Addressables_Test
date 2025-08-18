
using System;
using UnityEngine;

/// <summary>
/// Нужен для определения из вне статуса Mute (пример button, toggle)
/// </summary>
public abstract class CS_AbsTriggerStatusMute : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract event Action OnUpdateMuteData;
    
    public abstract void SetStatusMuteNotUseEvent(bool isMute);
    public abstract bool GetStatusMute();
}
