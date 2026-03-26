using System;
using UnityEngine;

/// <summary>
/// Триггер на то, что было нажатие по подсказке
/// (разными способоми надо детектить, т.к button сбрасывает фокус выбраного обьекта
/// и по этому не всегда подходит из за этого) 
/// </summary>
public abstract class AbsTriggerClickElementVariantText : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract event Action OnButtonClick;
}
