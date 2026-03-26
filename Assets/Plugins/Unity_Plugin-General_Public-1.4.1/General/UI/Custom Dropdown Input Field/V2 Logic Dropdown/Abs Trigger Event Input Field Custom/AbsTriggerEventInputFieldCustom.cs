using System;
using UnityEngine;

/// <summary>
/// Нужен для отслеживания состоянии открытия, закрытия и ввода текста в Input Field
/// </summary>
public abstract class AbsTriggerEventInputFieldCustom : MonoBehaviour
{
    public abstract event Action OnOpenInputField;
    public abstract event Action OnCloseInputField;
    public abstract event Action<string> OnUpdateText;
}
