using System;
using UnityEngine;

/// <summary>
/// Он будет получать список сцен(уровней) которые нужно добавить
/// И затем каждой сцене будет создовать UI (используя как ключ имя этой сцены)
/// </summary>
public class CreatorPrefabUI : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField] 
    private StorageSceneName _sceneLevel;

    [SerializeField] 
    private KeyStoragePrefabSceneUI _storagePrefabUI;

    [SerializeField] 
    private GameObject _parent;

    
    private void Awake()
    {
        if (_storagePrefabUI.IsInit == false)
        {
            _storagePrefabUI.OnInit += OnInitStoragePrefabSceneUI;
        }

        CheckInit();
    }
    
    private void OnInitStoragePrefabSceneUI()
    {
        if (_storagePrefabUI.IsInit == true) 
        {
            _storagePrefabUI.OnInit -= OnInitStoragePrefabSceneUI;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_storagePrefabUI.IsInit == true)
        {
            _isInit = true;
            OnInit?.Invoke();
        }
    }
    
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
