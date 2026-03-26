using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Берет все ключи из указ. SO и присваивает каждому interectabl статус bool.
/// Как итог, при выборе ключа у Dropdown, то InputField будет или включен или выключен
/// Имеет список исключений, на случай если надо указать другое состояние interectabl у определенного ключа
/// </summary>
public class DropdownReactionSelectInteractableInputField : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private Dropdown _dropdown;

    /// <summary>
    /// дефолтное состояние Interectable у компонента
    /// </summary>
    [SerializeField]
    private bool _defaultStatusInteractable = true;
    
    
    /// <summary>
    /// Исключение, на случай, если по этому ключу будет другой статус у Interectable
    /// (а bool - включен или нет)
    /// </summary>
    [SerializeField]
    private List<AbsKeyData<GetDataSO_KeyDropdown, bool>> _listExceptionInterectable = new List<AbsKeyData<GetDataSO_KeyDropdown, bool>>();
    
    private Dictionary<string, bool> _exceptioninterectabl = new Dictionary<string, bool>();
    
    [SerializeField]
    private List<InputField> _targetInputField;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (var VARIABLE in _listExceptionInterectable)
        {
            _exceptioninterectabl.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
        }
        
        _dropdown.onValueChanged.AddListener(OnUpdateSelect);
        OnUpdateSelect(_dropdown.value);

        _isInit = true;
        OnInit?.Invoke();
    }
    
    private void OnUpdateSelect(int id)
    {
        if (_exceptioninterectabl.ContainsKey(_dropdown.options[id].text) == true)
        {
            foreach (var VARIABLE in _targetInputField)
            {
                VARIABLE.interactable = _exceptioninterectabl[_dropdown.options[id].text];
            }    
        }
        else
        {
            foreach (var VARIABLE in _targetInputField)
            {
                VARIABLE.interactable = _defaultStatusInteractable;
            }    
        }
    }
    
    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(OnUpdateSelect);
    }
}
