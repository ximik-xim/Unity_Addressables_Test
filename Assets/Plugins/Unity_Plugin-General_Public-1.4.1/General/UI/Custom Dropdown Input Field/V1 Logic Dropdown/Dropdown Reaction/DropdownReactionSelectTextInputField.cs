using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    
/// <summary>
/// При переключении Dropdown будет устанавливать определенный текст в указанный InputField
/// Есть список исключений, если надо по ключу устанавливать определенный текст
/// Если нету ключа в списке исключений, то в качестве текста который будет вставлен в InputField, будет использоваться сам ключ
/// </summary>
public class DropdownReactionSelectTextInputField : MonoBehaviour
{
    
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private Dropdown _dropdown;
    
    /// <summary>
    /// Исключение, на случай, если по этому ключу не надо ничего устанавливать
    /// (а bool - это очищать ли строку при этом значении)
    /// </summary>
    [SerializeField]
    private List<AbsKeyData<GetDataSO_KeyDropdown, bool>> _listExceptionDontSetText = new List<AbsKeyData<GetDataSO_KeyDropdown, bool>>();
    private Dictionary<string, bool> _exceptionDontSetText = new Dictionary<string, bool>();
    
    /// <summary>
    /// Исключение, на случай, если надо установить не сам ключ, а какой то текст по этому ключу
    /// </summary>
    [SerializeField]
    private List<AbsKeyData<GetDataSO_KeyDropdown, string>> _listExceptionSetText = new List<AbsKeyData<GetDataSO_KeyDropdown, string>>();
    private Dictionary<string, string> _exceptionSetText = new Dictionary<string, string>();

    [SerializeField]
    private List<InputField>  _inputField;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (var keyException in _listExceptionDontSetText)
        {
            _exceptionDontSetText.Add(keyException.Key.GetData().GetKey(), keyException.Data);
        }
        
        foreach (var keyException in _listExceptionSetText)
        {
            _exceptionSetText.Add(keyException.Key.GetData().GetKey(), keyException.Data);
        }
        
        _dropdown.onValueChanged.AddListener(OnUpdateSelect);
        OnUpdateSelect(_dropdown.value);
        
        
        
        _isInit = true;
        OnInit?.Invoke();
    }
    
    private void OnUpdateSelect(int id)
    {
        if (_exceptionDontSetText.ContainsKey(_dropdown.options[id].text) == false) 
        {
            string textInsert = "";
        
            if (_exceptionSetText.ContainsKey(_dropdown.options[id].text) == true)
            {
                textInsert = _exceptionSetText[_dropdown.options[id].text];
            }
            else
            {
                textInsert = _dropdown.options[id].text;
            }

            foreach (var VARIABLE in _inputField)
            {
                VARIABLE.text = textInsert;
            }
        }
        else
        {
            if (_exceptionDontSetText[_dropdown.options[id].text] == true) 
            {
                foreach (var VARIABLE in _inputField)
                {
                    VARIABLE.text = "";
                }
            }
        }
    }
}
