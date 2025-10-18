using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Тут будут UI префабы, по ключу - который будет являться именем сцены
/// </summary>
public class KeyStoragePrefabSkinSceneLevelUI : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField] 
    private AbsSkinSceneLevelUI _defPrefabUI;

    [SerializeField] 
    private AbsExceptionsListInKeyStoragePrefabSkinSceneLevelUI _keyException;

    private Dictionary<string, AbsSkinSceneLevelUI> _exceptionData = new Dictionary<string, AbsSkinSceneLevelUI>();

    private void Awake()
    {
        if (_keyException.IsInit == false)
        {
            _keyException.OnInit += OnInitListExceptions;
        }

        CheckInit();
    }

    private void OnInitListExceptions()
    {
        if (_keyException.IsInit == true)
        {
            _keyException.OnInit -= OnInitListExceptions;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_keyException.IsInit == true)
        {
            foreach (var VARIABLE in _keyException.GetListExceptions())
            {
                _exceptionData.Add(VARIABLE.Key.GetKey(), VARIABLE.Data);
            }

            _isInit = true;
            OnInit?.Invoke();
        }
    }

    public AbsSkinSceneLevelUI GetPrefabUI(KeyNameScene key)
    {
        if (_exceptionData.ContainsKey(key.GetKey()) == true)
        {
            return _exceptionData[key.GetKey()];
        }

        return _defPrefabUI;
    }
}
