using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Нужен для отслеживания состоянии InputField
/// (должен находиться на том же GM что и InputField)
/// </summary>
[RequireComponent(typeof(InputField))]
public class TriggerEventInputFieldCustomInputField : AbsTriggerEventInputFieldCustom, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private InputField _inputField;

    public override event Action OnOpenInputField;
    public override event Action OnCloseInputField;
    public override event Action<string> OnUpdateText;

    private void Awake()
    {
        _inputField.onEndEdit.AddListener(OnEndEdit);
        _inputField.onValueChanged.AddListener(OnTextChanged);
    }

    private void OnTextChanged(string text)
    {
        OnUpdateText?.Invoke(text);
    }

    private void OnEndEdit(string arg0)
    {
        OnCloseInputField?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnOpenInputField?.Invoke();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnCloseInputField?.Invoke();
    }

    private void OnDestroy()
    {
        _inputField.onEndEdit.RemoveListener(OnEndEdit);
        _inputField.onValueChanged.RemoveListener(OnTextChanged);
    }
}
