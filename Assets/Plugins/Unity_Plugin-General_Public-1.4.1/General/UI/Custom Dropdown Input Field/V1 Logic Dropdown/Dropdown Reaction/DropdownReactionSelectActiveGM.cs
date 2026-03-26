using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Берет все ключи из указ. SO и присваивает каждому GM статус bool.
/// Как итог, при выборе ключа у Dropdown, то GM будет или включен или выключен
/// Имеет список исключений, на случай если надо указать другое состояние GM у определенного ключа
/// </summary>
public class DropdownReactionSelectActiveGM : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private Dropdown _dropdown;

    [SerializeField]
    private AbsGetListKeyDropdown _storageKeyDropdown;

    private List<string> _listKey = new List<string>();
    
    [SerializeField]
    private ListActionGmActiveAndDisactive _listGmSelectTargetKey;
    
    [SerializeField]
    private ListActionGmActiveAndDisactive _listGmDontSelectTargetKey;
    

    private void Awake()
    {
        if (_storageKeyDropdown.IsInit == false) 
        {
            _storageKeyDropdown.OnInit += OnInitAbsGetListKeyDropdown;
        }
        else
        {
            Init();
        }
        
    }

    private void OnInitAbsGetListKeyDropdown()
    {
        if (_storageKeyDropdown.IsInit == true)
        {
            _storageKeyDropdown.OnInit -= OnInitAbsGetListKeyDropdown;
            
            Init();
        }
    }

    private void Init()
    {
        foreach (var VARIABLE in _storageKeyDropdown.GetListKeyDropdown())
        {
            _listKey.Add(VARIABLE.GetKey());
        }
        
        _dropdown.onValueChanged.AddListener(OnUpdateSelect);
        OnUpdateSelect(_dropdown.value);

        _isInit = true;
        OnInit?.Invoke();
    }
    
    private void OnUpdateSelect(int id)
    {
        if (_listKey.Contains(_dropdown.options[id].text) == true)
        {
            _listGmSelectTargetKey.StartAction();
        }
        else
        {
            _listGmDontSelectTargetKey.StartAction();
        }
    }
    
    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(OnUpdateSelect);
    }
}
