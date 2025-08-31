using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Изменяет разрешение канваса, так что бы оно было равно разрешению камеры в пространстве unity
/// </summary>
public class CamerScalerSizeUnityCameraSize : CameraLogic2UpdateInfo
{
    [SerializeField] 
    private CanvasScaler _scaler;
    public override void UpdateData(DataEventUpdateCameraV2 data)
    {
        _scaler.scaleFactor = 1 / data.Multiplier;
    }
}
