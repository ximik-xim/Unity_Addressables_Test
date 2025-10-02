using System;
using UnityEngine;

/// <summary>
/// Буду запускать, когда надо будет удалить UI Task
/// </summary>
public class LogicDestroyTaskUI : MonoBehaviour
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
