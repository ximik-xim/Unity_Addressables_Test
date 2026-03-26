
using System;
using UnityEngine;
using UnityEngine.UI;

public class EventAddButtonClick : AbsEventAddElement
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action<int> OnAddElement;
    
    [SerializeField]
    private Button _button; 
    
    private void Awake()
    {
        _button.onClick.AddListener(ButtonClick);
        OnInit?.Invoke();
    }

    private void ButtonClick()
    {
        OnAddElement?.Invoke(1);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
