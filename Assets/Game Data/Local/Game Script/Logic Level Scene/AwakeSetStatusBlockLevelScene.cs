using System;
using UnityEngine;

/// <summary>
/// Нужен что бы установить или снять блокировку с уровня при запуске
/// </summary>
public class AwakeSetStatusBlockLevelScene : MonoBehaviour
{
    [SerializeField]
    private GetPatchIntStorageBlockScene _getPatchIntStorageBlockScene;
    
    [SerializeField]
    private KeyNameSceneInGetDataSO_KeyReferenceScene _keyScene;

    [SerializeField]
    private bool _isBlock;
    
    private void Awake()
    {
        _keyScene.StartInit();
        
        if (_getPatchIntStorageBlockScene.IsInit == false)
        {
            _getPatchIntStorageBlockScene.OnInit += OnInitGetDkoPatch;
        }
        
        if (_keyScene.IsInit == false)
        {
            _keyScene.OnInit += OnInitGetKeyScene;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_getPatchIntStorageBlockScene.IsInit == true)
        {
            _getPatchIntStorageBlockScene.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void OnInitGetKeyScene()
    {
        if (_keyScene.IsInit == true)
        {
            _keyScene.OnInit -= OnInitGetKeyScene;
            CheckInit();
        }
    }
    
    
    private void CheckInit()
    {
        if (_getPatchIntStorageBlockScene.IsInit == true && _keyScene.IsInit == true)
        {
            _getPatchIntStorageBlockScene.SetStatus(_keyScene.GetSceneName(), _isBlock);
        }
    }
}
