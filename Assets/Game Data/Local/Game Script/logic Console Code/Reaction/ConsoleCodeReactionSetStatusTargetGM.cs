
using System;
using UnityEngine;

/// <summary>
/// Реакция на код, которая включает или отключ указ GM
/// </summary>
public class ConsoleCodeReactionSetStatusTargetGM : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private bool _active;
    
    [SerializeField]
    private string _code;

    [SerializeField]
    private GetDKOPatch _dkoPatchInputConsole;
    
    [SerializeField]
    private InputConsoleCode _inputConsoleCode;
    
    
    private void Awake()
    {
        if (_inputConsoleCode == null)
        {
            _dkoPatchInputConsole.gameObject.SetActive(true);
            if (_dkoPatchInputConsole.Init == false)
            {
                _dkoPatchInputConsole.OnInit += OnInitGetDkoPatch;
            }
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
        if (_inputConsoleCode == null)
        {
            if (_dkoPatchInputConsole.Init == true)
            {
                _inputConsoleCode = _dkoPatchInputConsole.GetDKO<DKODataInfoT<InputConsoleCode>>().Data;

                _inputConsoleCode.OnInputCode += OnInputCode;
            }
        }
        else
        {
            _inputConsoleCode.OnInputCode += OnInputCode;
        }
    }

    private void OnInputCode(string obj)
    {
        if (obj.ToUpper() == _code.ToUpper())
        {
            _gameObject.SetActive(_active);
        }
    }


    private void OnDestroy()
    {
        if (_inputConsoleCode != null) 
        {
            _inputConsoleCode.OnInputCode -= OnInputCode;
        }
        
    }
}
