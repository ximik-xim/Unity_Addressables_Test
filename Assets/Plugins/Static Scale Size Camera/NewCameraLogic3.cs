    using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Изменяет размер камеры для сохранения соотношения разрешения и сохранения одинаковой видимости обьекта на разных разрешениях
    /// </summary>
public class NewCameraLogic3 : MonoBehaviour
{
[SerializeField]
    private float _targetWidht = 0f;


    [SerializeField] private Camera _camera;
     private float _sizeCamera;
     [SerializeField] 
     private Vector2 _currentScreenResolution;
    private void Awake()
    {
        if (_camera.orthographic == true)
        {
            _targetWidht = _camera.orthographicSize * _currentScreenResolution.x / _currentScreenResolution.y * 2f;
        }
    }

    private void Update()
    {
        // float width = Screen.width;
        // float height = Screen.height;
        
        float width = _camera.pixelWidth;
        float height = _camera.pixelHeight;
        
        float size = _targetWidht / (width / height * 2f);
        Debug.Log(size);
        _camera.orthographicSize = size;
        
    }
}
