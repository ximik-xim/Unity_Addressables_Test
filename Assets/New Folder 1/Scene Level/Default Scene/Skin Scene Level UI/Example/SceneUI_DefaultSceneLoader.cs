using System;
using UnityEngine;

/// <summary>
/// Префаб обложки сцены для загрузки через AbsSceneLoader
/// </summary>
public class SceneUI_DefaultSceneLoader : AbsSkinSceneLevelUI
{
    [SerializeField] 
    private ButtonSceneLoaderNameDefault _sceneLoader;
    
    [SerializeField]
    private DKOKeyAndTargetAction _dko;
    
    private KeyNameScene _keyNameScene;

    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;


    public override void SetNameScene(KeyNameScene nameScene)
    {
        _sceneLoader.SetNameScene(nameScene.GetKey());
        _keyNameScene = nameScene;
        
        _isInit = true;
        OnInit?.Invoke();
    }

    public override KeyNameScene GetName()
    {
        return _keyNameScene;
    }

    public override DKOKeyAndTargetAction GetSceneUIDKO()
    {
        return _dko;
    }
}
