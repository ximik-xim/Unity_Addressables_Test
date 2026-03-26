using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен что бы была возможность управлять списком выводимых подсказок
/// </summary>
public class GetListVariantTextBuffer : AbsGetListVariantText
{
    [SerializeField]
    private AbsGetListVariantText _getListKeyDropdown;

    [SerializeField]
    private List<string> _bufferListVariant = new List<string>();

    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;


    private void Awake()
    {
        foreach (var VARIABLE in _getListKeyDropdown.GetVariantText())
        {
            _bufferListVariant.Add(VARIABLE);
        }
    }

    public override List<string> GetVariantText()
    {
        return _bufferListVariant;
    }

    public void ClearAllVariantText()
    {
        _bufferListVariant.Clear();
    }

    public void AddListVariant(List<string> variantList)
    {
        foreach (var VARIABLE in variantList)
        {
            _bufferListVariant.Add(VARIABLE);
        }
    }
}
