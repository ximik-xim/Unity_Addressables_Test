using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public enum CustomButtonClickTypeClick
{
    OnPointerClick,
    OnPointerDown,
    OnPointerUp,
}
    
/// <summary>
/// Триггер на нажатие на обьект(замена button)
/// т.к в отличии от button,toggle и т.д тут нету логики ISelectable(которая автоматически меняет фокус на нажатый button, и логику родитель-ребенок её пофиг) 
/// </summary>
public class CustomButtonClick : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler
{
    public bool Interactable = true;
        
    private Graphic _targetGraphic;

    [SerializeField]
    private Color _normalColor = Color.white;
    [SerializeField]
    private Color _highlightedColor = new Color(0.9f, 0.9f, 0.9f);
    [SerializeField]
    private Color _pressedColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField]
    private Color _disabledColor = Color.gray;

    [SerializeField]
    [Range(1f,5f)]
    private float _colorMultiplier = 1f;
    
    [SerializeField]
    private float _fadeDuration = 0.1f;

    /// <summary>
    /// Когда сработает event об нажатии
    /// OnPointerClick - когда нажали и отпусти на том же обьекте
    /// OnPointerDown - когда нажали на обьект
    /// OnPointerUp - когда нажали и немного сместили курсор
    /// </summary>
    [SerializeField]
    private CustomButtonClickTypeClick _typeClickEvent = CustomButtonClickTypeClick.OnPointerClick;
    
    public event Action OnButtonClick;

    void Awake()
    {
        if (_targetGraphic == null)
        {
            _targetGraphic = GetComponent<Graphic>();
        }

        SetColor(_normalColor);
    }

    private void OnEnable()
    {
        if (Interactable == false)
        {
            return;
        }        
        SetColor(_normalColor);
    }

    void SetColor(Color tintColor, bool instant = false)
    {
        _targetGraphic.CrossFadeColor(tintColor * _colorMultiplier, instant ? 0f : _fadeDuration, true, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Interactable == false)
        {
            return;
        }
        SetColor(_highlightedColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable == false)
        {
            return;
        }        
        SetColor(_normalColor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Interactable == false)
        {
            return;
        }
        SetColor(_pressedColor);
        
        if (_typeClickEvent == CustomButtonClickTypeClick.OnPointerDown) 
        {
            OnButtonClick?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Interactable == false)
        {
            return;
        }
        SetColor(_highlightedColor);
        
        if (_typeClickEvent == CustomButtonClickTypeClick.OnPointerUp) 
        {
            OnButtonClick?.Invoke();
        }
    }

    public void SetInteractable(bool value)
    {
        Interactable = value;
        SetColor(Interactable ? _normalColor : _disabledColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Interactable == false)
        {
            return;
        }

        if (_typeClickEvent == CustomButtonClickTypeClick.OnPointerClick) 
        {
            OnButtonClick?.Invoke();
        }
        
    }
}


