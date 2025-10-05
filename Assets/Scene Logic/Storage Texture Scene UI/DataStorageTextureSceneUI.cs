using UnityEngine;

[System.Serializable]
public class DataStorageTextureSceneUI
{
    [SerializeField]
    private Texture _texture;
    
    [SerializeField] 
    private Color _color = Color.white;

    public Texture Texture => _texture;
    public Color Color => _color;
}

