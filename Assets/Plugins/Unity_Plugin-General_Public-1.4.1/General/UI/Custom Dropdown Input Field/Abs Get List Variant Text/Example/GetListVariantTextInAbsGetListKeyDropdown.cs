using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужно что бы заполнить ключами из KeyDropdown
/// </summary>
public class GetListVariantTextInAbsGetListKeyDropdown : AbsGetListVariantText
{
    [SerializeField]
    private AbsGetListKeyDropdown _getListKeyDropdown;

    public override bool IsInit => _getListKeyDropdown.IsInit;
    public override event Action OnInit
    {
        add
        {
            _getListKeyDropdown.OnInit += value;
        }
        remove
        {
            _getListKeyDropdown.OnInit -= value;
        }
    }
    public override List<string> GetVariantText()
    {
        List<string> listKey = new List<string>();
        foreach (var VARIABLE in _getListKeyDropdown.GetListKeyDropdown())
        {
            listKey.Add(VARIABLE.GetKey());
        }

        return listKey;
    }
}
