using UnityEngine;

/// <summary>
/// Данные об текстуре и цвете для обложки сцены
/// </summary>
[System.Serializable]
public class DataStorageTextureAndColorSkinSceneLevelUI
{
    [SerializeField]
    private Texture _texture;
    
    [SerializeField] 
    private Color _color = Color.white;

    public Texture Texture => _texture;
    public Color Color => _color;
}

