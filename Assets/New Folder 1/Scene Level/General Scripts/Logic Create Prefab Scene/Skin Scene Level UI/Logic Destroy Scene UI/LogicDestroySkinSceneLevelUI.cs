using System;
using UnityEngine;

/// <summary>
/// Логика для запуска уничтожения GM обложки сцены
/// </summary>
public class LogicDestroySkinSceneLevelUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectDestroy;

    public event Action OnStartDestroy;
    
    public void StartDestroyObject()
    {
        OnStartDestroy?.Invoke();
       
        Destroy(_objectDestroy);
    }
}
