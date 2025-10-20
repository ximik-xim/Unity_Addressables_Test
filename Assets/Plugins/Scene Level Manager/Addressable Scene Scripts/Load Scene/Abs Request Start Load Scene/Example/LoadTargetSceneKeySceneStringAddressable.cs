using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// Нужен что бы удобно указать ключ сцены через хранилище с ключами
/// (в качестве ключа будет имя заданное в Addressable (он же PrimaryKey))
/// </summary>
public class LoadTargetSceneKeySceneStringAddressable : AbsRequestStartLoadSceneAddressable
{
    public override event Action OnInit;
    public override bool IsInit => _isInit;
    private bool _isInit = false;

    [SerializeField]
    private LoadTargetSceneAddressables _loadSceneAddressables;
        
    [SerializeField] 
    private KeyNameSceneInGetDataSO_KeyReferenceScene _keyNameScene;

    private void Awake()
    {
        if (_loadSceneAddressables.IsInit == false)
        {
            _loadSceneAddressables.OnInit += OnInitLoadSceneAddressables;
        }
        
        if (_keyNameScene.IsInit == false)
        {
            _keyNameScene.OnInit += OnInitKeyNameScene;
        }
        
        CheckInit();
    }

    private void OnInitLoadSceneAddressables()
    {
        if (_loadSceneAddressables.IsInit == true)
        {
            _loadSceneAddressables.OnInit -= OnInitLoadSceneAddressables;
            
            CheckInit();
        }
    }

    private void OnInitKeyNameScene()
    {
        if (_keyNameScene.IsInit == true)
        {
            _keyNameScene.OnInit -= OnInitKeyNameScene;
            
            CheckInit();
        }
    }
    

    private void CheckInit()
    {
        if (_loadSceneAddressables.IsInit == true && _keyNameScene.IsInit == true)
        {
            Init();
        }
    }

    private void Init()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public override GetServerRequestData<AsyncOperationHandle<SceneInstance>> StartLoadScene()
    {
        return _loadSceneAddressables.StartLoadScene(_keyNameScene.GetKeySceneName().GetKey());
    }
}
