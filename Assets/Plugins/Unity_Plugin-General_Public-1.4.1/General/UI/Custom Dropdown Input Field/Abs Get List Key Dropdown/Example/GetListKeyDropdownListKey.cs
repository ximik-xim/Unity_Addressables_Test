using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Получаю ключи из списка, который заполняю вручную
/// </summary>
public class GetListKeyDropdownListKey : AbsGetListKeyDropdown
{
    [SerializeField]
    private List<GetDataSO_KeyDropdown> _listKey;

    public override bool IsInit => true;
    public override event Action OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<KeyDropdown> GetListKeyDropdown()
    {
        List<KeyDropdown> list = new List<KeyDropdown>();
        foreach (var VARIABLE in _listKey)
        {
            list.Add(VARIABLE.GetData());
        }

        return list;
    }
}
