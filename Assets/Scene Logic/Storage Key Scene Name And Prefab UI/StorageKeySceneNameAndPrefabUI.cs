using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// При добавл сцены в StorageSceneName, будет создовать префаб для этой сцены
/// и добавлять DKO этого префаба в свое хранилеще, по ключу сцены
/// </summary>
public class StorageKeySceneNameAndPrefabUI : MonoBehaviour
{
    [SerializeField]
    private StorageSceneName _sceneName;

    [SerializeField]
    private Dictionary<string, DKOKeyAndTargetAction> _scenes = new Dictionary<string, DKOKeyAndTargetAction>();


    [SerializeField]
    private FabricPrefabUI _fabricPrefabUI;

    [SerializeField] 
    private GetDataSODataDKODataKey _getKeyDestroyObj;

    private void Awake()
    {
        if (_fabricPrefabUI.IsInit == false)
        {
            _fabricPrefabUI.OnInit += OnInitFabricPrefabUI;
        }

        CheckInit();
    }

    private void OnInitFabricPrefabUI()
    {
        if (_fabricPrefabUI.IsInit == true)
        {
            _fabricPrefabUI.OnInit -= OnInitFabricPrefabUI;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_fabricPrefabUI.IsInit == true)
        {
            Init();
        }
    }

    private void Init()
    {
        _sceneName.OnAddScene += AddScene;
        _sceneName.OnRemoveScene += RemoveScene;
        
        CheckScene();
    }

    private void CheckScene()
    {
        var listScene = _sceneName.GetAllScene();
        foreach (var VARIABLE in listScene)
        {
            AddScene(VARIABLE);
        }
    }

    public void AddScene(KeyNameScene keyNameScene)
    {
        var prefab= _fabricPrefabUI.CreatePrefabUI(keyNameScene);
        prefab.SetNameScene(keyNameScene);
        
        _scenes.Add(keyNameScene.GetKey(), prefab.GetSceneUIDKO());
    }
    public void RemoveScene(KeyNameScene keyNameScene)
    {
        var dko = _scenes[keyNameScene.GetKey()];

        var data = (DKODataInfoT<LogicDestroySceneUI>)dko.KeyRun(_getKeyDestroyObj.GetData());
        data.Data.StartDestroyObject();
        
        _scenes.Remove(keyNameScene.GetKey());
       
    }
    
    public bool IsThereScene(KeyNameScene keyNameScene)
    {
        return _scenes.ContainsKey(keyNameScene.GetKey());
    }

    public DKOKeyAndTargetAction GetSceneUI_DKO(KeyNameScene keyNameScene)
    {
        return _scenes[keyNameScene.GetKey()];
    }

    private void OnDestroy()
    {
        _sceneName.OnAddScene -= AddScene;
        _sceneName.OnRemoveScene -= RemoveScene;
    }
}
