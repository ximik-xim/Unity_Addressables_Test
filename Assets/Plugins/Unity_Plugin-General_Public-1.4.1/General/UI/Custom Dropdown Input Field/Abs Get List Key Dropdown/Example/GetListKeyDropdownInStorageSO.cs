using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Получаю список ключей из указ SO с ключами
/// </summary>
public class GetListKeyDropdownInStorageSO : AbsGetListKeyDropdown
{
    [SerializeField]
    private SO_Data_KeyDropdown _storageKeyDropdown;

    public override bool IsInit => true;
    public override event Action OnInit;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override List<KeyDropdown> GetListKeyDropdown()
    {
        return _storageKeyDropdown.GetAllData();
    }
}
