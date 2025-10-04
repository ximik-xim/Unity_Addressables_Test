using UnityEngine;

/// <summary>
/// Тут будет находиться имя сцены к которой будем обращаться
/// </summary>
[System.Serializable]
public class KeyNameScene 
{
    public KeyNameScene()
    {
        
    }
    
    public KeyNameScene(string nameScene)
    {
        this._nameScene = nameScene;
    }
    
    [SerializeField]
    private string _nameScene;

    public string GetKey()
    {
        return _nameScene;
    }
}
