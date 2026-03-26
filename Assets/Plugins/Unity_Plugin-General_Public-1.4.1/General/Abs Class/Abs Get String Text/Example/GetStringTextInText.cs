using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Вернет текст из UI компонента Text
/// </summary>
public class GetStringTextInText : AbsGetStringText
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private Text _text;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override string GetStringText()
    {
        return _text.text;
    }
}
