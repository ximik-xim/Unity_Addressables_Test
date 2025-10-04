using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AssetReferenceSceneCustom : AssetReferenceT<SceneAsset>
{
    public AssetReferenceSceneCustom(string guid) : base(guid)
    {
    }
}
