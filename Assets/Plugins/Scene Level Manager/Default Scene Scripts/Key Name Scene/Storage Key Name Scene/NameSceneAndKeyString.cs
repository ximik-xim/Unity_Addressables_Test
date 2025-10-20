using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class NameSceneAndKeyString
{
#if UNITY_EDITOR
    [OnChangedCall(nameof(StartSetName))]
    
    /// <summary>
    ///Ссылка на сцену
    /// Можно оставить пусто и вручную указ ключ
    /// (к примеру, если ключ не стандартный и его получ не через ссылку на сцену)
    /// </summary>
    [SerializeField]
    private SceneAsset _sceneAsset;
#endif
    
    /// <summary>
    /// Не путать с ключем для сцены, в случае assetRef(имя сцена != ключ для её загрузки)
    /// </summary>
    [SerializeField]
    private string _nameScene;
    
    [SerializeField]
    private string _keyNameScene;

#if  UNITY_EDITOR

    [SerializeField]
    private bool _isAutoSetNameScene = true;
    
    [SerializeField]
    private bool _isAutoSetKeyScene = true;
    
    public void StartSetName()
    {
        if (_sceneAsset != null)
        {
            if (_isAutoSetNameScene == true) 
            {
                _nameScene = _sceneAsset.name;    
            }

            if (_isAutoSetKeyScene == true) 
            {
                _keyNameScene = _sceneAsset.name;
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
    
    public string GetKey()
    {
        
#if  UNITY_EDITOR
        StartSetName();
#endif
        
        return _keyNameScene;
    }
}
