using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoaderAddressable : AbsSceneLoader
{
    public override bool IsInit => _loadTargetSceneAddressables.IsInit;

    public override event Action OnInit
    {
        add
        {
            _loadTargetSceneAddressables.OnInit += value;
        }
        remove
        {
            _loadTargetSceneAddressables.OnInit -= value;
        }
    }
    

    [SerializeField]
    private LoadTargetSceneAddressables _loadTargetSceneAddressables;
    

    public override void LoadScene(int numberScene)
    {
        Debug.LogError("Addressables не поддерживает загрузку по номеру сцены, только по её ключу");
    }

    public override void LoadScene(string keyNameScene)
    {
        _loadTargetSceneAddressables.StartLoadScene(keyNameScene);

    }
}