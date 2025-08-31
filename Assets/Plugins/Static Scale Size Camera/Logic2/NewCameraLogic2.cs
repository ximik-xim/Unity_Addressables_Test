using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Изменяет размер камеры в зависимости от размера разрешение окна
///(Если измениться размерешение разрешения окна в ширену, то и обзор камеры увел. в ширену)
///(Если измениться размерешение разрешения окна в высоту, то и обзор камеры увел. в высоту)
/// Данный скрипт рабботает с высотой до 8192 и шереной до 8192, т.к если поставить больше, то  _camera.pixelHeight и Screen.height вернут максимально лишь 8192, хотя _camera.aspect выщит верно(по реальным данным)
/// </summary>
public class NewCameraLogic2 : MonoBehaviour
{
   [SerializeField] 
    private Camera _camera;

    /// <summary>
    /// Изначальный множитель размера камеры
    /// </summary>
    [SerializeField]
    private float _initialSize;

    /// <summary>
    /// Изначальная ширена и высота отображаемой области камеры(в виде нормалезованных значений)
    /// </summary>
    [SerializeField]
    private Vector2 _startRectWidhtAndHeight;
    
    /// <summary>
    /// Изначальная ширена и высота камеры (в пикселя)(та ширена и высота под которую был сверстан UI игры) 
    /// </summary>
    [SerializeField] 
    private Vector2 _scalePixelWidhtAndHeight = new Vector2(720, 1280);
        
    /// <summary>
    /// Тип вычесления границ камеры(если они были установлены изначально, тоесть Rect.heigt и Rect.Widht не были равны 1)
    /// Multiplying - это умножение на нормалезованное число, которое было уст при старте
    /// Value - это числовое значение высоты границы, которое вычесл при старте
    /// </summary>
    [SerializeField] 
    private TypeScaleH _typeScaleBorders = TypeScaleH.Multiplying;

    /// <summary>
    /// Максимальная ширена и высота  камеры в пикселях(при достижении этих значений, изображение будет просто обрезаться)
    /// </summary>
    [SerializeField] 
    private Vector2 _maxScalePixelCamera = new Vector2(8192, 8192);

    /// <summary>
    /// Тип скеилинга(увелечения) камеры
    /// None - это означает камера ни как не будет увеличивать или уменьшаться стараясь сохранить разрешение
    /// Resolution - этот режим значит, что камера будет увел. или уменьшаться сохраняя изнач соотношение сторон изображение
    /// </summary>
    [SerializeField] 
    private TypeScaleXX _typeScaleCameraFromResolution;
    
    /// <summary>
    /// Авто центровка изображения
    /// </summary>
    [SerializeField]
    private bool _avtoCentr = false;


    private Vector2 CurrentScaleScreen = new Vector2(0, 0);
    private float multiplier = 1f;
    private float realHeight = 0f;
    private float realWidht = 0f;

    private Vector2 _cameraRectWidthAndHeight;
    private Vector2 _cameraPixelWidthAndHeight;

    [SerializeField] 
    private List<CameraLogic2UpdateInfo> UpdateData;
    private void Awake()
    {
        _initialSize = _camera.orthographicSize;
        _startRectWidhtAndHeight.y = _camera.rect.height;

        switch (_typeScaleBorders)
        {
            case TypeScaleH.Multiplying:
            {
                if (_camera.rect.height > 1f)
                {
                    _camera.rect = new Rect(_camera.rect.x, _camera.rect.y, _camera.rect.width, 1f);
                }
                
                _startRectWidhtAndHeight.y = _camera.rect.height;
                
                
                if (_camera.rect.width > 1f)
                {
                    _camera.rect = new Rect(_camera.rect.x, _camera.rect.y, 1f, _camera.rect.height);
                }

                _startRectWidhtAndHeight.x = _camera.rect.width;

            } break;
                
            case TypeScaleH.Value:
            {
                if (_camera.rect.height > 1f)
                {
                    _camera.rect = new Rect(_camera.rect.x, _camera.rect.y, _camera.rect.width, 1f);
                }


                var bordersNormal = 1f - _camera.rect.height;

                //startHeight = bordersNormal * _camera.pixelHeight / _camera.rect.height;
                _startRectWidhtAndHeight.y = bordersNormal * _scalePixelWidhtAndHeight.y;
                
                if (_camera.rect.width > 1f)
                {
                    _camera.rect = new Rect(_camera.rect.x, _camera.rect.y, 1f, _camera.rect.height);
                }

                
                
                
                var bordersNormalWidht = 1f - _camera.rect.width;

                //startHeight = bordersNormal * _camera.pixelHeight / _camera.rect.height;
                _startRectWidhtAndHeight.x = bordersNormalWidht * _scalePixelWidhtAndHeight.x;

            } break;
        }
    }

    private void Update()
    {
            if (CurrentScaleScreen.x != Screen.width || CurrentScaleScreen.y != Screen.height)
            {
                CurrentScaleScreen = new Vector2(Screen.width, Screen.height);
                //Нужен только в случ скеилинга от маштаба экрана, как множитель
                 multiplier = 1f;


                if (_typeScaleBorders == TypeScaleH.Multiplying)
                {
                    //_camera.rect = new Rect(_camera.rect.x, _camera.rect.y,_startRectWidhtAndHeight.x , _startRectWidhtAndHeight.y);


                    realHeight = _camera.pixelHeight / _camera.rect.height;
                    realWidht = _camera.pixelWidth / _camera.rect.width;

                    if (_typeScaleCameraFromResolution == TypeScaleXX.Resolution)
                    {
                        multiplier = MaxDifference(realHeight, realWidht);
                    }
                }

                if (_typeScaleBorders == TypeScaleH.Value)
                {
                    

                    
                    realHeight = _camera.pixelHeight / _camera.rect.height;
                    realWidht = _camera.pixelWidth / _camera.rect.width;
                    

                    if (_typeScaleCameraFromResolution == TypeScaleXX.Resolution)
                    {
                        multiplier = MaxDifference(realHeight, realWidht);
                    }
                    
                }

//                Debug.Log("multiplier =  " + multiplier);
                _cameraRectWidthAndHeight = new Vector2(_camera.rect.width, _camera.rect.height);
                _cameraPixelWidthAndHeight = new Vector2(_camera.pixelWidth, _camera.pixelHeight);

                foreach (var VARIABLE in UpdateData)
                {
                    DataEventUpdateCameraV2 dataEventUpdateCameraV2 = new DataEventUpdateCameraV2(multiplier);
                    
                    VARIABLE.UpdateData(dataEventUpdateCameraV2);
                }
            }
            


            _camera.rect = new Rect(_camera.rect.x, _camera.rect.y, WidthCalculation(), HeightCalculation());

            float sizeCamera = _initialSize * (_camera.pixelHeight / _scalePixelWidhtAndHeight.y);
            _camera.orthographicSize = sizeCamera * multiplier;

            if (_avtoCentr == true)
            {
                var cameraPosX = (1f - _camera.rect.width) / 2;
                var cameraPosY = (1f - _camera.rect.height) / 2;
                
                _camera.rect = new Rect(cameraPosX, cameraPosY, _camera.rect.width, _camera.rect.height);
            }
//            Debug.Log("_camera.pixelHeight = "+_camera.pixelHeight);
//            Debug.Log("_camera.pixelWidth = "+_camera.pixelWidth);
    }



    private float HeightCalculation()
    {
        var height = 0f;
        
        if (_typeScaleBorders == TypeScaleH.Multiplying)
        {
            var sizeVisebleCamera = 0f;
            if ((_maxScalePixelCamera.y / multiplier) > realHeight * _startRectWidhtAndHeight.y)
            {
                sizeVisebleCamera = realHeight * _startRectWidhtAndHeight.y;
            }
            else
            {
                sizeVisebleCamera = _maxScalePixelCamera.y / multiplier;
            }

            height = _cameraRectWidthAndHeight.y - (_cameraPixelWidthAndHeight.y - sizeVisebleCamera) / realHeight;

            if (height > 1)
            {
                height = 1;
            }
            
           return height;
        }
            
            
            
            
            
            
        if (_typeScaleBorders == TypeScaleH.Value)
        {
            if (_maxScalePixelCamera.y / multiplier > realHeight - _startRectWidhtAndHeight.y)
            {
                height = (realHeight - _startRectWidhtAndHeight.y / multiplier) / realHeight;
                    
                if (height > 1)
                {
                    height = 1;
                }

                return height;
            }
            else
            {
                height = _maxScalePixelCamera.y / realHeight / multiplier;
                if (height > 1)
                {
                    height = 1;
                }
                return height;
            }
        }

        return 0;
    }

    private float WidthCalculation()
    {
        var width = 0f;
        
            if (_typeScaleBorders == TypeScaleH.Multiplying)
            {

      
                var sizeVisebleCamera = 0f;
                
                if ((_maxScalePixelCamera.x / multiplier) > realWidht * _startRectWidhtAndHeight.x)
                {
                    sizeVisebleCamera = realWidht * _startRectWidhtAndHeight.x;
                }
                else
                {
                    sizeVisebleCamera = _maxScalePixelCamera.x / multiplier;
                }

                width = _cameraRectWidthAndHeight.x - (_cameraPixelWidthAndHeight.x - sizeVisebleCamera) / realWidht;

                if (width > 1)
                {
                    width = 1;
                }
                
                return width;

            }

           
            if (_typeScaleBorders == TypeScaleH.Value)
            {
                if (_maxScalePixelCamera.x / multiplier > realWidht - _startRectWidhtAndHeight.x)
                {
                      width = (realWidht - _startRectWidhtAndHeight.x / multiplier) / realWidht;
                    
                    if (width > 1)
                    {
                        width = 1;
                    }
                    
                    return width;
                }
                else
                {
                    width = _maxScalePixelCamera.x / realWidht / multiplier;
                    if (width > 1)
                    {
                        width = 1;
                    }
                    return width;
                }
                
            }

            return 0;
    }
    
    /// <summary>
    /// Расчитывает кофицента для увелечения камеры и границ изображения
    /// </summary>
    /// <returns></returns>
    private float MaxDifference(float RealHeight, float RealWidht)
    {
        var heightDifference = _scalePixelWidhtAndHeight.y / RealHeight;
        var widthDifference = _scalePixelWidhtAndHeight.x / RealWidht;
        
        if (heightDifference >= widthDifference)
        {
            return heightDifference;

        }

        return widthDifference;
    }


    public void AddUpdateInfo(CameraLogic2UpdateInfo updateInfo)
    {
        UpdateData.Add(updateInfo);
        
        DataEventUpdateCameraV2 dataEventUpdateCameraV2 = new DataEventUpdateCameraV2(multiplier);
        updateInfo.UpdateData(dataEventUpdateCameraV2);
    }
    
    public void AddRemove(CameraLogic2UpdateInfo updateInfo)
    {
        for (int i = 0; i <UpdateData.Count ; i++)
        {
            if (UpdateData[i]==updateInfo)
            {
                UpdateData.RemoveAt(i);
                return;
            }
        }
    }
}

public enum TypeScaleH
{
    Multiplying,
    Value,
}

public enum TypeScaleXX
{
    None,
    Resolution
}

public class DataEventUpdateCameraV2
{
    public DataEventUpdateCameraV2(float multiplier)
    {
        _multiplier = multiplier;
    }

    private float _multiplier;
    public float Multiplier => _multiplier;
}
