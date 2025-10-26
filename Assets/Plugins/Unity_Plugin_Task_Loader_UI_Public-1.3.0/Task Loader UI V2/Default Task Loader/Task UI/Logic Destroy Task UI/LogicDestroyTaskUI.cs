using System;
using UnityEngine;

/// <summary>
/// Буду запускать, когда надо будет удалить UI Task
/// (по идеи, если нужно будет что бы кто то уничтожился вместе с Task,
/// то надо будет подпис на OnStartDestroy и при его срабатывании запустить логику уничтожения у этого обьекта)
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
