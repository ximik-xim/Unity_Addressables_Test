using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// Нужен что бы удобно указать ключ через инспектор
/// </summary>
public class LoadTargetSceneAssetReference : AbsLoadTargetSceneKey
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
    private AssetReference _keyNameScene;

    public override GetServerRequestData<SceneInstance> StartLoadScene()
    {
        return _loadSceneAddressables.StartLoadScene(_keyNameScene);
    }
}
