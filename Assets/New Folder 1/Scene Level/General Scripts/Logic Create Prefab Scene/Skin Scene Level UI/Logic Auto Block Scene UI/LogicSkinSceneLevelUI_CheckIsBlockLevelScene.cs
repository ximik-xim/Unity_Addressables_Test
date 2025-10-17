using System;
using UnityEngine;

/// <summary>
/// Логика автоматической проверки, заблокирована ли сцена
/// (если да, то обложка тоже будет заблокирована)
/// </summary>
public class LogicSkinSceneLevelUI_CheckIsBlockLevelScene : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _getDkoPatch;

    [SerializeField]
    private AbsSkinSceneLevelUI _sceneUI;

    private StorageBlockScene _storageBlockScene;

    [SerializeField]
    private GameObject _gameObjectBlock;

    [SerializeField]
    private bool _checkOnEnable = true;
    
    private void Awake()
    {
        if (_getDkoPatch.Init == false)
        {
            _getDkoPatch.OnInit += OnInitGetDkoPatch;
        }
        
        if (_sceneUI.IsInit == false)
        {
            _sceneUI.OnInit += OnInitSceneUI;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_getDkoPatch.Init == true)
        {
            _getDkoPatch.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void OnInitSceneUI()
    {
        if (_sceneUI.IsInit == true)
        {
            _sceneUI.OnInit -= OnInitSceneUI;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_getDkoPatch.Init == true && _sceneUI.IsInit == true)  
        {
            _storageBlockScene = _getDkoPatch.GetDKO<DKODataInfoT<StorageBlockScene>>().Data;
            
            InitData();
        }
    }

    private void InitData()
    {
        _storageBlockScene.OnUpdateData += OnUpdateData;
        
        CheckIsBlockLevelScene();
    }

    private void OnUpdateData()
    {
        CheckIsBlockLevelScene();
    }

    private void CheckIsBlockLevelScene()
    {
        if (_storageBlockScene.GetIsBlock(_sceneUI.GetName()) == true) 
        {
            _gameObjectBlock.SetActive(true);
        }
        else
        {
            _gameObjectBlock.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        if (_checkOnEnable == true) 
        {
            if (_storageBlockScene != null)
            {
                CheckIsBlockLevelScene();
            }
        }
    }

    private void OnDestroy()
    {
        if (_storageBlockScene != null) 
        {
            _storageBlockScene.OnUpdateData -= OnUpdateData;    
        }
    }
}
