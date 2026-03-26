using System;
using UnityEngine;
using UnityEngine.UI;

public class EventRemoveButtonClick : AbsEventRemoveElement
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnRemoveElement;
    
    [SerializeField]
    private Button _button; 
    
    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
        OnInit?.Invoke();
    }

    private void ButtonClick()
    {
        OnRemoveElement?.Invoke();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
