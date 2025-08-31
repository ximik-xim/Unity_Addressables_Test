using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Изменяет размер камеры в зависимости от размера разрешение окна
///(Если измениться размерешение разрешения окна в ширену, то и обзор камеры увел. в ширену)
///(Если измениться размерешение разрешения окна в высоту, то и обзор камеры увел. в высоту)
/// Данный скрипт рабботает с высотой до 8192 и шереной до 8192, т.к если поставить больше, то  _camera.pixelHeight и Screen.height вернут максимально лишь 8192, хотя _camera.aspect выщит верно(по реальным данным)
/// </summary>
public class NewCameraLogic : MonoBehaviour
{
    [SerializeField] 
    private Camera _camera;

    /// <summary>
    /// Изначальный множитель размера камеры
    /// </summary>
    [SerializeField]
    private float initialSize;
    
    /// <summary>
    /// Изначальная высота камеры (в пикселя)
    /// </summary>
    [SerializeField]
    private float ScaleHeightPixel;
    
    private void Awake()
    {
        initialSize = _camera.orthographicSize;
    }

    private void Update()
    {
        if (_camera.orthographic)
        {
            Debug.Log(initialSize + "*" + _camera.pixelHeight + "/" + ScaleHeightPixel);
            float constantWidthSize = initialSize * (_camera.pixelHeight/ ScaleHeightPixel );
            _camera.orthographicSize = constantWidthSize;
        }
    }
    
}
