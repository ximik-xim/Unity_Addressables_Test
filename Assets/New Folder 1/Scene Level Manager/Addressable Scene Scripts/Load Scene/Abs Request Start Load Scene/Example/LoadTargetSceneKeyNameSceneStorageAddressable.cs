using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// Нужен что бы удобно указать ключ сцены через хранилище с ключами
/// (в качестве ключа будет Asset Ref)
/// </summary>
public class LoadTargetSceneKeyNameSceneStorageAddressable : AbsRequestStartLoadSceneAddressable
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
        
    [SerializeField] 
    private GetDataSO_KeyReferenceScene _keyNameScene;

    public override GetServerRequestData<AsyncOperationHandle<SceneInstance>> StartLoadScene()
    {
        return _loadSceneAddressables.StartLoadScene(_keyNameScene.GetData().GetRefScene());
    }
}
