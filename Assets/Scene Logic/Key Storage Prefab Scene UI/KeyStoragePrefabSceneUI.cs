using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Тут будут UI префабы, по ключу - который будет являться именем сцены
/// </summary>
public class KeyStoragePrefabSceneUI : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField] 
    private SceneUI _defPrefabUI;

    [SerializeField] 
    private List<AbsKeyData<GetDataSO_NameScene, SceneUI>> _keyException;

    private Dictionary<string, SceneUI> _exceptionData = new Dictionary<string, SceneUI>();


    private void Awake()
    {
        foreach (var VARIABLE in _keyException)
        {
            _exceptionData.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
        }

        _isInit = true;
        OnInit?.Invoke();
    }

    public SceneUI GetPrefabUI(KeyNameScene key)
    {
        if (_exceptionData.ContainsKey(key.GetKey()) == true)
        {
            return _exceptionData[key.GetKey()];
        }

        return _defPrefabUI;
    }
}
