using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadTargetSceneSetKey : AbsLoadTargetSceneKey
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

    public override GetServerRequestData<SceneInstance> StartLoadScene()
    {
        return _loadSceneAddressables.StartLoadScene(_keyScene);
    }
}
