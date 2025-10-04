using System;
using UnityEngine;

/// <summary>
/// Создает экземпляр префаба SceneUI на сцене
/// и указываем этому экземпляру родителя
/// </summary>
public class FabricPrefabUI : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;

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

    public AbsSceneUI CreatePrefabUI(KeyNameScene key)
    {
        var prefabUI = _storagePrefabUI.GetPrefabUI(key);
        var example = Instantiate(prefabUI, _parent.transform);

        return example;
    }
}
