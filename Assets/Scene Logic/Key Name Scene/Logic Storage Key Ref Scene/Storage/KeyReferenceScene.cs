using UnityEngine;

[System.Serializable]
public class KeyReferenceScene 
{
    [SerializeField]
    private AssetReferenceSceneCustom _refScene;

    public AssetReferenceSceneCustom GetRefScene()
    {
        return _refScene;
    }
}
