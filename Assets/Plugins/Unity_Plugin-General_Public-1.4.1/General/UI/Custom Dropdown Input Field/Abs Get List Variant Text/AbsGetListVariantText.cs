using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Абстракция нужная для получения списка вариантов подсказок
/// </summary>
public abstract class AbsGetListVariantText : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract List<string> GetVariantText();
}
