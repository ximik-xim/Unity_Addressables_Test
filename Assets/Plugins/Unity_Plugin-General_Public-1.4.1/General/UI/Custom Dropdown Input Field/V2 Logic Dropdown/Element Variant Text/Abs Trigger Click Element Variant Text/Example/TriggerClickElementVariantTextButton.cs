using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Триггер нажатия на подсказку через кнопку
/// </summary>
public class TriggerClickElementVariantTextButton : AbsTriggerClickElementVariantText
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnButtonClick;

    [SerializeField]
    private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
        OnInit?.Invoke();
    }

    private void ButtonClick()
    {
        OnButtonClick?.Invoke();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
