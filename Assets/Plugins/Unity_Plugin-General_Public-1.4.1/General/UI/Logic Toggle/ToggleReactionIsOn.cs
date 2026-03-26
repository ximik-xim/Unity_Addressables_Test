using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Нормальный обработчик event на событие у Toggle об его выборе или снятии с выбранного
/// </summary>
public class ToggleReactionIsOn : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;
    
    [SerializeField]
    public UnityEvent _eventToggleSelect;
    
    [SerializeField]
    public UnityEvent _eventToggleRemove;
    
    private void Awake()
    {
        _toggle.onValueChanged.AddListener(OnTriggeToggle);
        OnTriggeToggle(_toggle.isOn);
    }

    private void OnTriggeToggle(bool arg0)
    {
        if (_toggle.isOn == true)
        {
            _eventToggleSelect?.Invoke();
        }
        else
        {
            _eventToggleRemove?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnTriggeToggle);
    }
}