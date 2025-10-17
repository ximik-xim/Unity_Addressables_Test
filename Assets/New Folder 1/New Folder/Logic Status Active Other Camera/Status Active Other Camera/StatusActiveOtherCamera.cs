using System;
using UnityEngine;


/// <summary>
/// Нужен что бы можно было контролировать отключения Main Camera на других сценах
/// 
/// </summary>
public class StatusActiveOtherCamera : MonoBehaviour
{
    [SerializeField]
    private bool _activeOtherCamera = false;
    public event Action OnUpdateStatus;
    
    public void SetStatusActiveOtherCamera(bool active)
    {
        _activeOtherCamera = active;
        OnUpdateStatus?.Invoke();
    }


    public bool GetStatusActiveOtherCamera()
    {
        return _activeOtherCamera;
    }
}
