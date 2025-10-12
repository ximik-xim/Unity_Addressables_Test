using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadTargetSceneStringKey : AbsLoadTargetSceneKey
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
    private string _keyNameScene;

    public override GetServerRequestData<AsyncOperationHandle<SceneInstance>> StartLoadScene()
    {
        return _loadSceneAddressables.StartLoadScene(_keyNameScene);
    }
}
