using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Установка текста в обычное поле Text
/// </summary>
public class GetAndSetTextDefault : AbsGetAndSetText
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private Text _text;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override string GetText()
    {
        return _text.text;
    }

    public override void SetText(string text)
    {
        _text.text = text;
    }
    
    
}
