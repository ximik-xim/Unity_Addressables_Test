using UnityEngine;

/// <summary>
/// Он будет получать список сцен(уровней) которые нужно добавить
/// И затем каждой сцене будет создовать UI (используя как ключ имя этой сцены)
/// </summary>
public class CreatorPrefabUI : MonoBehaviour
{
    [SerializeField] 
    private StorageSceneName _sceneLevel;

    [SerializeField] 
    private KeyStoragePrefabSceneUI _storagePrefabUI;

    [SerializeField] 
    private GameObject _parent;

    public void StartCreateUI()
    {
        //получ. список сцен
        var listScene = _sceneLevel.GetAllScene();

        foreach (var VARIABLE in listScene)
        {
            var prefabUI = _storagePrefabUI.GetPrefabUI(VARIABLE);
            var example = Instantiate(prefabUI, _parent.transform);
            
            example.SetNameScene(VARIABLE);
        }
        
    }
}
