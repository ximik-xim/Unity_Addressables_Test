using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Получает список ключей который будет засунут в Dropdown из AbsGetListKeyDropdown
/// </summary>
public class DropdownGetOptionDataInAbsGetListKeyDropdown : AbsDropdownGetOptionData
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;

    [SerializeField]
    private AbsGetListKeyDropdown _absGetListKeyDropdown;

    private void Awake()
    {
        if (_absGetListKeyDropdown.IsInit == false) 
        {
            _absGetListKeyDropdown.OnInit += OnInitAbsGetListKeyDropdown;
        }
        else
        {
            Init();
        }
        
    }

    private void OnInitAbsGetListKeyDropdown()
    {
        if (_absGetListKeyDropdown.IsInit == true)
        {
            _absGetListKeyDropdown.OnInit -= OnInitAbsGetListKeyDropdown;
            
            Init();
        }
    }

    private void Init()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public override List<Dropdown.OptionData> GetListKeyDropdown()
    {
        List<Dropdown.OptionData> optionData = new List<Dropdown.OptionData>();
        foreach (var VARIABLE in _absGetListKeyDropdown.GetListKeyDropdown())
        {
            optionData.Add(new Dropdown.OptionData(VARIABLE.GetKey()));
        }

        return optionData;

    }
}
