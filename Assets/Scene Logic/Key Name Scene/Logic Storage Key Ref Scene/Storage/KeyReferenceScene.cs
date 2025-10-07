using UnityEngine;

[System.Serializable]
public class KeyReferenceScene
{
    /// <summary>
    /// Не путать с ключем для сцены, в случае assetRef(имя сцена != ключ для её загрузки)
    /// </summary>
    [SerializeField]
    private string _nameScene;
    
#if  UNITY_EDITOR
    [OnChangedCall(nameof(StartSetName))]
#endif
    [SerializeField]
    private AssetReferenceSceneCustom _refScene;

    public AssetReferenceSceneCustom GetRefScene()
    {
        return _refScene;
    }
    
#if  UNITY_EDITOR
    public void StartSetName()
    {
        if (_nameScene != null)
        {
            if (_refScene.editorAsset != null)
            {
                _nameScene = _refScene.editorAsset.name;    
            }
        }
    }
#endif

    public string GetNameScene()
    {
        
#if  UNITY_EDITOR
        StartSetName();
#endif
        
        return _nameScene;
    }
}
