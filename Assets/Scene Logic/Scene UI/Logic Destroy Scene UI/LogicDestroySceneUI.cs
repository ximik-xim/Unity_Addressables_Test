using System;
using UnityEngine;

public class LogicDestroySceneUI : MonoBehaviour
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
