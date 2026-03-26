using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI логики консольных кодов
/// </summary>
public class InputConsoleCodeUI : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _dkoPatchInputConsole;
    
    private InputConsoleCode _inputConsoleCode;

    [SerializeField]
    private Button _button;

	[SerializeField]
    private InputField _inputField;

    
    private void Awake()
    {
        if (_dkoPatchInputConsole.Init == false)
        {
            _dkoPatchInputConsole.OnInit += OnInitGetDkoPatch;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_dkoPatchInputConsole.Init == true)
        {
            _dkoPatchInputConsole.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_dkoPatchInputConsole.Init == true)
        {
            _inputConsoleCode = _dkoPatchInputConsole.GetDKO<DKODataInfoT<InputConsoleCode>>().Data;
            _button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        _inputConsoleCode.SetCode(_inputField.text);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
