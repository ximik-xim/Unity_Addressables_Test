using System;
using UnityEngine;

/// <summary>
/// Абстракция для работы с текстовым полем
/// (что бы можно было исп разные текстовые поля)
/// </summary>
public abstract class AbsGetAndSetText : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract string GetText();
    public abstract void SetText(string text);
}
