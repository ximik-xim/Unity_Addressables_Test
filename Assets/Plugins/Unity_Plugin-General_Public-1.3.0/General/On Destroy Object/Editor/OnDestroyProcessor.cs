using UnityEditor;
using UnityEngine;

/// <summary>
/// Нужен для отслеживания момента удаления фаила
/// </summary>
class OnDestroyProcessor : UnityEditor.AssetModificationProcessor
{
    // Опр. какой тип данных будем отслеживать
    static System.Type _type = typeof(ScriptableObject);

    // Опр формат фаила через путь
    static string _fileEnding = ".asset";

    public static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions _)
    {
        if (path.EndsWith(_fileEnding) == false)
        {
            return AssetDeleteResult.DidNotDelete;
        }

        var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
        if (assetType != null && (assetType == _type || assetType.IsSubclassOf(_type)))
        {
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            var test = asset as ICustomDestroySO;
            if (test != null)
            {
                test.CustomDestroy();
            }
        }

        return AssetDeleteResult.DidNotDelete;
    }
}



