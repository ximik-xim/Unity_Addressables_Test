using UnityEngine;

public class SceneUI_Addressable : AbsSceneUI
{
    [SerializeField] 
    private LoadTargetSceneSetKey _sceneLoader;

    [SerializeField]
    private DKOKeyAndTargetAction _dko;
    
    public override void SetNameScene(KeyNameScene nameScene)
    {
        _sceneLoader.SetKeyScene(nameScene.GetKey());
    }

    public override DKOKeyAndTargetAction GetSceneUIDKO()
    {
        return _dko;
    }
}
