using System;
using UnityEngine;

/// <summary>
/// Отвечает за отслеживания включения клавиатуры на телефоне
/// (В случае необходимости тестирования в редакторе,
/// необходимо вручную дергать переключатель о том что клава включена)
/// </summary>
public class TriggerOpenKeyboardMobileDefault : AbsTriggerOpenKeyboardMobile
{
    public override bool KeyboardIsVisible => _keyboardIsVisible;
    private bool _keyboardIsVisible = false;
    public override event Action<bool> OnUpdateStatusKeyboardIsVisible;

#if UNITY_EDITOR
    [SerializeField]
    private bool _keyboardIsVisibleEditor;
#endif
    
    private void Awake()
    {
        OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _keyboardIsVisible = _keyboardIsVisibleEditor;
        OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
    }
#endif

#if !UNITY_EDITOR
    private void Update()
    {
        if (TouchScreenKeyboard.visible != _keyboardIsVisible)
        {
            _keyboardIsVisible = TouchScreenKeyboard.visible;
            OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
        }
        
    }
#endif
    
    
}
