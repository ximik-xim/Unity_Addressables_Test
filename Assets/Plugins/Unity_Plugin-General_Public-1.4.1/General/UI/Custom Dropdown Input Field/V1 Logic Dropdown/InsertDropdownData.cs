using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Засовывает список ключей в Dropdown
/// </summary>
public class InsertDropdownData : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private AbsDropdownGetOptionData _absDropdownGetOptionData;

    [SerializeField]
    private Dropdown _dropdown;

    //Буду ли очищать список Option у Dropdown, прежде чем туда засунуть данные
    [SerializeField]
    private bool _clearData = true;

    
    
    private void Awake()
    {
        if (_absDropdownGetOptionData.IsInit == false) 
        {
            _absDropdownGetOptionData.OnInit += OnInitAbsDropdownGetOptionData;
        }
        else
        {
            Init();
        }
    }

    private void OnInitAbsDropdownGetOptionData()
    {
        if (_absDropdownGetOptionData.IsInit == true)
        {
            _absDropdownGetOptionData.OnInit -= OnInitAbsDropdownGetOptionData;
            
            Init();
        }
    }

    private void Init()
    {
        if (_clearData == true) 
        {
            _dropdown.ClearOptions();
        }

        _dropdown.AddOptions(_absDropdownGetOptionData.GetListKeyDropdown());
        
        //Вручную вызываю event, т.к данные обновились(а по умолчанию, dropdown НЕ вызывает этот event после загр. новых данных, т.к считает что id не изменился)
        _dropdown.onValueChanged.Invoke(_dropdown.value);
        
        _isInit = true;
        OnInit?.Invoke();
    }
}
