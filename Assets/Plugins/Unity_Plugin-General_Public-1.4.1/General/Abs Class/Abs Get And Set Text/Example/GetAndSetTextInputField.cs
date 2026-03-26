using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Установка текста в поле InputField
/// </summary>
public class GetAndSetTextInputField : AbsGetAndSetText
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private InputField _inputField;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override string GetText()
    {
        return _inputField.text;
    }

    public override void SetText(string text)
    {
        _inputField.text = text;
    }
}
