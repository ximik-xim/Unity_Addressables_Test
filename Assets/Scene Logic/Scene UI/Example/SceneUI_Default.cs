using UnityEngine;

public class SceneUI_Default : AbsSceneUI
{
    [SerializeField] 
    private ButtonSceneLoaderNameDefault _sceneLoader;

    [SerializeField]
    private DKOKeyAndTargetAction _dko;
    
    public override void SetNameScene(KeyNameScene nameScene)
    {
        _sceneLoader.SetNameScene(nameScene.GetKey());
    }

    public override DKOKeyAndTargetAction GetSceneUIDKO()
    {
        return _dko;
    }
}
