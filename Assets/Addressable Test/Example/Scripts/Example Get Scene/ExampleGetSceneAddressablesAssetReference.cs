using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class ExampleGetSceneAddressablesAssetReference : MonoBehaviour
{
    [SerializeField]
    private AssetReference _keyNameScene;
    
    [SerializeField]
    private LoadTargetSceneAddressables _loadTargetSceneAddressables;
    
    private void Awake()
    {
        if (_loadTargetSceneAddressables.IsInit == false)
        {
            _loadTargetSceneAddressables.OnInit += OnInitGetData;
            return;
        }

        InitGetData();
    }

    private void OnInitGetData()
    {
        if (_loadTargetSceneAddressables.IsInit == true)
        {
            _loadTargetSceneAddressables.OnInit -= OnInitGetData;
            InitGetData();
        }

    }

    private void InitGetData()
    {
        Debug.Log("Послан запрос на загрузку сцены");
        _loadTargetSceneAddressables.StartLoadScene(_keyNameScene);
    }
}
