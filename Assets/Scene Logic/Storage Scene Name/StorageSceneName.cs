using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список имен сцен, которые будут загружены
/// </summary>
public class StorageSceneName : MonoBehaviour
{
    [SerializeField]
    private List<KeyNameScene> _scenes;

    public event Action OnUpdatecene;
    
    public event Action<KeyNameScene> OnAddScene;
    public event Action<KeyNameScene> OnRemoveScene;
    
    public void AddScene(KeyNameScene name)
    {
        _scenes.Add(name);
        
        OnAddScene?.Invoke(name);
        OnUpdatecene?.Invoke();
    }
    
    public void RemoveScene(KeyNameScene name)
    {
        _scenes.Remove(name);
        
        OnRemoveScene?.Invoke(name);
        OnUpdatecene?.Invoke();
    }
    
    public bool IsThereScene(KeyNameScene name)
    {
        return _scenes.Contains(name);
    }
    
    public IReadOnlyList<KeyNameScene> GetAllScene()
    {
        return _scenes;
    }
}
