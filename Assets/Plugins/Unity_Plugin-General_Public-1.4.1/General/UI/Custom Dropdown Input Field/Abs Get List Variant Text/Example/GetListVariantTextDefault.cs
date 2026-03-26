
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Задаем список подсказок через инспектор
/// </summary>
public class GetListVariantTextDefault : AbsGetListVariantText
{
    [SerializeField]
    private List<string> _variantElement;


    public override bool IsInit => true;
    public override event Action OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<string> GetVariantText()
    {
        return _variantElement;
    }
}
