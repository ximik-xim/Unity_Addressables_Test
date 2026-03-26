using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Логика выноса выброннаого элемента над клавиаутрой ввода(для мобилок)
/// Можно через шаблоны сделать указ. где будут наход. элементы
/// А можно просто через логику установки обьекта над клавиатурой
/// </summary>
public class LogicSetSelectObjectOnTopKeyboard : MonoBehaviour
{
    [SerializeField] 
    private List<AbsTriggerOpenKeyboardMobile>  _triggerOpenKeyboardMobile;
    
    /// <summary>
    /// Игнорировать ли обьекты у которых нету скрипта с настройками
    /// Если включено, то у тех обьектов что нету скрипта с настройками, просто не буду трогать вообще
    /// Если выключено, то тогда к таким обьектам будет применена дефолтная логика
    /// </summary>
    [SerializeField]
    private bool _isIgnoreObjectDontSettings = false;
    
    /// <summary>
    /// Нужна то бы через неё определять реальный размер экрана(в пикселях и т.д)
    /// </summary>
    [SerializeField]
    private Camera _camera;
    
    /// <summary>
    /// Главный канвса(должен быть в режиме Screen Space Camera,
    /// что бы получить тек. видимый размер и для вычеслений позицй)
    /// </summary>
    [SerializeField]
    private Canvas _canvas;
    
    /// <summary>
    /// Нужен т.к скорее всего в конце нужно будет
    /// вынести обьект на отдельную панель, что бы явно было видно счем взаимодействую
    /// </summary>
    [SerializeField]
    private GameObject _panelParent;
    
    /// <summary>
    /// Отступ от клавиатуры (если размеры обьекта позволяют)
    /// </summary>
    [SerializeField]
    private float _offsetKeydownY = 10;
    
    /// <summary>
    /// Отступ от верха канваса (если размеры слишком большие)
    /// </summary>
    [SerializeField]
    private float _offsetTopCanvasY = 10;
    
    /// <summary>
    /// Образец желаймой позиции и размеров обьекта
    /// (не обязательно)
    /// </summary>
    [SerializeField]
    private RectTransform _sample;
    
    /// <summary>
    /// Выбран ли сейчас обьект
    /// </summary>
    private bool _isSelectItem;
    
    /// <summary>
    /// Сохраняю ссылку на выбранный обьект, что бы потом к нему же обратиться
    /// </summary>
    private GameObject _selectItem;
    
    /// <summary>
    /// Нужен что бы сохранить позицию GM до смещения
    /// </summary>
    private RectTransformUiElement _saveTransformTargetGM;
    
    /// <summary>
    /// Event Получили обьект и сейчас начнем уст у него размеры позицию и т.д
    /// </summary>
    public WrapperCustomEventPriorityT<DKOKeyAndTargetAction> OnStartSetPosition => OnEventStartSetPosition.WrapperCustomEventPriorityT;
    private HostCustomEventPriorityT<DKOKeyAndTargetAction> OnEventStartSetPosition = new HostCustomEventPriorityT<DKOKeyAndTargetAction>();
    
    /// <summary>
    /// Event Закончили устанавливать новую позицию у обьекта
    /// </summary>
    public WrapperCustomEventPriorityT<DKOKeyAndTargetAction> OnEndSetPosition => OnEventEndSetPosition.WrapperCustomEventPriorityT;
    private HostCustomEventPriorityT<DKOKeyAndTargetAction> OnEventEndSetPosition = new HostCustomEventPriorityT<DKOKeyAndTargetAction>();
    
    /// <summary>
    /// Event Начали возвращать обьект на исходную позицию
    /// </summary>
    public WrapperCustomEventPriorityT<DKOKeyAndTargetAction> OnStartReturnPosition => OnEventStartReturnPosition.WrapperCustomEventPriorityT;
    private HostCustomEventPriorityT<DKOKeyAndTargetAction> OnEventStartReturnPosition = new HostCustomEventPriorityT<DKOKeyAndTargetAction>();
    
    /// <summary>
    /// Event Закончили возвращать обьект на исходную позицию
    /// </summary>
    public WrapperCustomEventPriorityT<DKOKeyAndTargetAction> OnEndReturnPosition => OnEventEndReturnPosition.WrapperCustomEventPriorityT;
    private HostCustomEventPriorityT<DKOKeyAndTargetAction> OnEventEndReturnPosition = new HostCustomEventPriorityT<DKOKeyAndTargetAction>();
    
    private void Awake()
    {
        foreach (var VARIABLE in _triggerOpenKeyboardMobile)
        {
            VARIABLE.OnUpdateStatusKeyboardIsVisible += OnUpdateStatusKeyboardIsVisible;
        }
    }

    /// <summary>
    /// Получения высоты клавиатуры
    /// </summary>
    /// <returns></returns>
    private float GetHeightKeyboard()
    {
        float height = 0f;

        //Почти никогда не работает
        height = TouchScreenKeyboard.area.height;
        
        if (height != 0f)
        {
            return height;
        }
        
        //Получение через Java логику размеров клавы(работает только под андроид,
        //и возращает размер клавы в пикселях относительно реального размера экрана) 
#if UNITY_ANDROID && !UNITY_EDITOR
    using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
    using (var view = activity.Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView"))
    using (var rect = new AndroidJavaObject("android.graphics.Rect"))
    {
        view.Call("getWindowVisibleDisplayFrame", rect);

        int visibleHeight = rect.Call<int>("height");
        int totalHeight = view.Call<int>("getHeight");

        height = Mathf.Max(0, totalHeight - visibleHeight);

        if (height != 0f)
        {
            return height;
        }
        
    }
#endif

        //Получаю данные о том, насколько обрезан экран
        var viewport = _camera.rect;
        //получаю видимую высоту экрана в пикселях
        float visibleHeightPx = _camera.pixelHeight;
        //получаю итоговую высоту экрана в пикселях
        float heighPhonePx = visibleHeightPx / viewport.height;

        return heighPhonePx / 2f;
    }
    
    private void OnUpdateStatusKeyboardIsVisible(bool keyboardIsVisible)
    {
        if (_isSelectItem == false) 
        {
            if (keyboardIsVisible == true)
            {
                GameObject currentGM = EventSystem.current.currentSelectedGameObject;
              
                if (currentGM != null) 
                {
                    if (_isIgnoreObjectDontSettings == true)
                    {
                        SettingsSelectObjectOnTopKeyboard data = currentGM.GetComponent<SettingsSelectObjectOnTopKeyboard>();
                        if (data != null && data.IsUseThisGM == true)
                        {
                            OnEventStartSetPosition.CustomEventPriorityT.Invoke(data.Dko);
                            
                            _isSelectItem = true;
                            _selectItem = currentGM;
                            SetCustomPosInGM(_selectItem);
                            
                            OnEventEndSetPosition.CustomEventPriorityT.Invoke(data.Dko);
                        }
                    }
                    else
                    {
                        SettingsSelectObjectOnTopKeyboard data = currentGM.GetComponent<SettingsSelectObjectOnTopKeyboard>();
                        if (data == null || data.IsUseThisGM == true)
                        {
                            _isSelectItem = true;
                            _selectItem = currentGM;
                            SetCustomPosInGM(_selectItem);
                        }
                    }
                }
            }
        }
        else
        {
            if (keyboardIsVisible == false)
            {
                SettingsSelectObjectOnTopKeyboard data = _selectItem.GetComponent<SettingsSelectObjectOnTopKeyboard>();
                if (data != null) 
                {
                    OnEventStartReturnPosition.CustomEventPriorityT.Invoke(data.Dko);
                }
                
                ReturnLastPos(_selectItem);
                _isSelectItem = false;
                _selectItem = null;
                
                if (data != null) 
                {
                    OnEventEndReturnPosition.CustomEventPriorityT.Invoke(data.Dko);
                }
            }
            
        }
    }
    
    

    /// <summary>
    /// Логика установки обьекта по примеру
    /// </summary>
    /// <param name="gm"></param>
    private void SetCustomPosInGM(GameObject gm)
    {
        SettingsSelectObjectOnTopKeyboard parametrSelectItem = gm.GetComponent<SettingsSelectObjectOnTopKeyboard>();
        if (parametrSelectItem != null)
        {
            if (parametrSelectItem.IsUseThisGM == true)
            {
                if (parametrSelectItem.IsUseModification == true)
                {
                    //Пытаюсь взять пример у самого обьекта
                    if (parametrSelectItem.Sample != null) 
                    {
                        SaveCurrentPos(gm);
                        
                        RectTransform rectTransformGM = gm.GetComponent<RectTransform>();
                        
                        _panelParent.SetActive(true);
                        rectTransformGM.parent = _panelParent.transform;
                        
                        rectTransformGM.SetSiblingIndex(parametrSelectItem.Sample.GetSiblingIndex());

                        rectTransformGM.anchorMin = parametrSelectItem.Sample.anchorMin;
                        rectTransformGM.anchorMax = parametrSelectItem.Sample.anchorMax;
                        rectTransformGM.pivot = parametrSelectItem.Sample.pivot;

                        rectTransformGM.anchoredPosition = parametrSelectItem.Sample.anchoredPosition;
                        rectTransformGM.sizeDelta = parametrSelectItem.Sample.sizeDelta;
                        
                        rectTransformGM.localPosition = parametrSelectItem.transform.localPosition;

                        if (parametrSelectItem.IsSetObjectPosTopKeyboard == true) 
                        {
                            SetTargetObjectTopKeyboard(gm);    
                        }
                        
                        rectTransformGM.parent = _panelParent.transform;
                 
                        return;
                    }
                    //Пытаюсь взять общий пример
                    else if (_sample != null) 
                    {
                        SaveCurrentPos(gm);
                        
                        RectTransform rectTransformGM = gm.GetComponent<RectTransform>();
                        
                        _panelParent.SetActive(true);
                        rectTransformGM.parent = _panelParent.transform;
                        
                        rectTransformGM.SetSiblingIndex(_sample.GetSiblingIndex());

                        rectTransformGM.anchorMin = _sample.anchorMin;
                        rectTransformGM.anchorMax = _sample.anchorMax;
                        rectTransformGM.pivot = _sample.pivot;

                        rectTransformGM.anchoredPosition = _sample.anchoredPosition;
                        rectTransformGM.sizeDelta = _sample.sizeDelta;

                        rectTransformGM.localPosition = _sample.transform.localPosition;

                        if (parametrSelectItem.IsSetObjectPosTopKeyboard == true)
                        {
                            SetTargetObjectTopKeyboard(gm);    
                        }
                        
                        rectTransformGM.parent = _panelParent.transform;
                        
                        return;
                    }
                    else
                    {
                        StartSetDefaultPos(gm);
                        return;
                    }
                }
                else
                {
                    StartSetDefaultPos(gm);
                    return;
                }
            }

        }
        else if (_isIgnoreObjectDontSettings == false) 
        {
            StartSetDefaultPos(gm);
            return;
        }
    }

    

    private void StartSetDefaultPos(GameObject gm)
    {
        SettingsSelectObjectOnTopKeyboard parametrSelectItem = gm.GetComponent<SettingsSelectObjectOnTopKeyboard>();
        if (parametrSelectItem != null)
        {
            if (parametrSelectItem.IsUseThisGM == true)
            {
                SetDefaultPos(gm);
                return;
            }
        }

        SetDefaultPos(gm);
        return;
    }



    /// <summary>
    /// Логика установки окна над клавиатурой
    /// или вверху Canvas(если весь обьект невлазиет)
    /// </summary>
    private void SetTargetObjectTopKeyboard(GameObject gm)
    {
        RectTransform rectTransformGM = gm.GetComponent<RectTransform>();
        RectTransform rectParentCanvas = _canvas.GetComponent<RectTransform>();

        rectTransformGM.parent = rectParentCanvas;
        

        //----------------------------------------------------------
        
        //Получаю данные о том, насколько обрезан экран
        var viewport = _camera.rect;
        //получаю видимую высоту экрана в пикселях
        float visibleHeightPx = _camera.pixelHeight;
        //получаю итоговую высоту экрана в пикселях
        float heighPhonePx = visibleHeightPx / viewport.height;

        //Насколько смещено изображение(если оно обрезано), относительно низа
        float offsetDownVisible = heighPhonePx * viewport.y;
        
        //Высота клавиатуры(отнял смещение, т.при вычеслении позиции на Canvas оно уже доходит обрезанное, т.к часть клавы находиться в неотоброжаймой зоне)
        float heightKeyBoardInCanvas = GetHeightKeyboard() - offsetDownVisible;
        
        //----------------------------------------------------------
        
        
        //расстояние от центра Pivot до нижний границы канваса(именно до нижней границы, это не есть вся высота канваса) 
        float distanceDownCanvas = rectParentCanvas.rect.height * rectParentCanvas.pivot.y;
        //Получаю именно координату Y нижний границы канваса
        float posDownCanvas = rectParentCanvas.position.y - distanceDownCanvas;
        
        //Координата верхней точки клавиатуры(на канвасе)
        float posTopKeyboard = posDownCanvas + heightKeyBoardInCanvas;
        
        //расстояние от центра Pivot до нижний границы у выбранного обьекта(именно до нижней границы, это не есть вся высота обьекта) 
        float distanceDownTargetGm = rectTransformGM.rect.height * rectTransformGM.pivot.y;
        //Координаты по Y выбранного обьекта, что бы он был над клавиатурой + смещение, что бы лучше виглядело
        float targetGmPosY = posTopKeyboard + distanceDownTargetGm + _offsetKeydownY;

        
        //координаты верхней точки канваса по Y
        float posTopCanvas = posDownCanvas + rectParentCanvas.rect.height;
        
        //расстояние от центра Pivot до верхней границы у выбранного обьекта
        float distanceTopTargetGm = rectTransformGM.rect.height * (1 - rectTransformGM.pivot.y);
        //позиция верхней точки выбранного обьекта, если его установим на клавиатурой
        float posTopTargetGm = targetGmPosY + distanceTopTargetGm;
        
        
        //используя данную формулу, получу координаты центра относ pivot (и пофиг на смещение pivot)
        float centrX = rectParentCanvas.rect.width / 2 - rectParentCanvas.rect.width * rectParentCanvas.pivot.x;

        //Если верхняя точка выбранного обьекта будет ниже верхней точки канваса
        if (posTopCanvas > posTopTargetGm)   
        {
            //То установим выбранный обьект над клавиатурой
            rectTransformGM.localPosition = new Vector3(centrX, targetGmPosY, 0);
        }
        else
        {
            //Иначе(если верхняя точка выбр. обьекта уходит за верхнюю точку канваса)
            //то перемещаю обьект в самую верхнюю точку канваса
            
            targetGmPosY = posTopCanvas - distanceTopTargetGm - _offsetTopCanvasY;
            rectTransformGM.localPosition = new Vector3(centrX, targetGmPosY, 0);
        }
    }
    

    /// <summary>
    /// Запускает логику для уст. обьекта над клавиатурой
    /// </summary>
    private void SetDefaultPos(GameObject gm)
    {
        SaveCurrentPos(gm);
        
        SetTargetObjectTopKeyboard(gm);
        
        _panelParent.SetActive(true);
        gm.transform.parent = _panelParent.transform;
    }

    
    private void SaveCurrentPos(GameObject gm)
    {
        RectTransform rectTransformGM = gm.GetComponent<RectTransform>();
                    
        _saveTransformTargetGM.parent = rectTransformGM.parent;
        _saveTransformTargetGM.siblingIndex = rectTransformGM.GetSiblingIndex();

        _saveTransformTargetGM.anchoredPosition = rectTransformGM.anchoredPosition;
        _saveTransformTargetGM.sizeDelta = rectTransformGM.sizeDelta;

        _saveTransformTargetGM.anchorMin = rectTransformGM.anchorMin;
        _saveTransformTargetGM.anchorMax = rectTransformGM.anchorMax;
        _saveTransformTargetGM.pivot = rectTransformGM.pivot;
    }


    /// <summary>
    /// Возвращает обьекту его размеры, позицию и т.д
    /// Которая была до старта 
    /// </summary>
    /// <param name="gm"></param>
    private void ReturnLastPos(GameObject gm)
    {
        if (gm != null) 
        {
            RectTransform rectTransformGM = gm.GetComponent<RectTransform>();

            rectTransformGM.SetParent(_saveTransformTargetGM.parent, false);
            rectTransformGM.SetSiblingIndex(_saveTransformTargetGM.siblingIndex);

            rectTransformGM.anchorMin = _saveTransformTargetGM.anchorMin;
            rectTransformGM.anchorMax = _saveTransformTargetGM.anchorMax;
            rectTransformGM.pivot = _saveTransformTargetGM.pivot;

            rectTransformGM.anchoredPosition = _saveTransformTargetGM.anchoredPosition;
            rectTransformGM.sizeDelta = _saveTransformTargetGM.sizeDelta;
        }
        
          _panelParent.SetActive(false);
        
    }

    private void OnDestroy()
    {
        foreach (var VARIABLE in _triggerOpenKeyboardMobile)
        {
            VARIABLE.OnUpdateStatusKeyboardIsVisible -= OnUpdateStatusKeyboardIsVisible;
        }
    }
}

[System.Serializable]
public struct RectTransformUiElement
{
    public Transform parent;
    public int siblingIndex;

    public Vector2 anchoredPosition;
    public Vector2 sizeDelta;

    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 pivot;
}