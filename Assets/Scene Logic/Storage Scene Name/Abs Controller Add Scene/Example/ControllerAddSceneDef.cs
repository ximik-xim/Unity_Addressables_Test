using System;
using UnityEngine;

public class ControllerAddScene : AbsControllerAddScene
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
    public override event Action OnComplitedAddScene;
    
    [SerializeField] 
    private AbsGetStorageKeyNameScene _sceneLevel;

    [SerializeField] 
    private StorageSceneName _storageScene;

    private void Awake()
    {
        if (_sceneLevel.IsInit == false)
        {
            _sceneLevel.OnInit += OnInitSceneLevel;
        }

        CheckInit();
    }
    
    private void OnInitSceneLevel()
    {
        if (_sceneLevel.IsInit == true) 
        {
            _sceneLevel.OnInit -= OnInitSceneLevel;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_sceneLevel.IsInit == true)
        {
            _isInit = true;
            OnInit?.Invoke();
        }
    }

    public override void StartAddScene()
    {
        //получ. список сцен
        var listScene = _sceneLevel.GetData().GetAllData();

        foreach (var VARIABLE in listScene)
        {
            _storageScene.AddScene(VARIABLE);
        }
        
    }
}
