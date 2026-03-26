using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Определяем надо ли удалять элемент через Toggle
/// </summary>
public class IsRemoveElementToggle : AbsIsRemoveElement
{
    public override bool IsInit => true;
    public override event Action OnInit;
    
    [SerializeField]
    private Toggle _toggle;
    
    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override bool IsRemoveElement()
    {
        return _toggle.isOn;
    }
}
