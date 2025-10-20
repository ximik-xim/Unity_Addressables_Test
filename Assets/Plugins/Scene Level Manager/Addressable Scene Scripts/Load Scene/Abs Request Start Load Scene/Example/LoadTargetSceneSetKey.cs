using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// Нужен что бы указать ключ из вне
/// </summary>
public class LoadTargetSceneSetKeyAddressable : AbsRequestStartLoadSceneAddressable
{
    public override event Action OnInit
    {
        add
        {
            _loadSceneAddressables.OnInit += value;
        }

        remove
        {
            _loadSceneAddressables.OnInit -= value;
        }
    }

    public override bool IsInit => _loadSceneAddressables.IsInit;

    [SerializeField]
    private LoadTargetSceneAddressables _loadSceneAddressables;
        
    [SerializeReference] 
    private object _keyScene;

    public void SetKeyScene(object keyScene)
    {
        _keyScene = keyScene;
    }

    public override GetServerRequestData<AsyncOperationHandle<SceneInstance>> StartLoadScene()
    {
        return _loadSceneAddressables.StartLoadScene(_keyScene);
    }
}
