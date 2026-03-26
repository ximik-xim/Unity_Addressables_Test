using System;
using UnityEngine;

/// <summary>
/// Триггер нажатия на подсказку через кастомную кнопку
/// (не сбрасывает фокус с ранее выбранного обьекта)
/// </summary>
public class TriggerClickElementVariantTextCustomButton : AbsTriggerClickElementVariantText
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnButtonClick;

    [SerializeField]
    private CustomButtonClick _button;

    private void Awake()
    {
        _button.OnButtonClick += ButtonClick;
        OnInit?.Invoke();
    }

    private void ButtonClick()
    {
        OnButtonClick?.Invoke();
    }

    private void OnDestroy()
    {
        _button.OnButtonClick -= ButtonClick;
    }
}
