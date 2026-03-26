using System;
using UnityEngine;

/// <summary>
/// Обертка, которая тригерит изменения статуса bool на кнопке
/// </summary>
public class GetBoolStatusButton : IGetBoolStatusMono
{
    [SerializeField]
    private BoolStatusButton _buttonSelect;
    
    public override bool IsInit => true;
    public override event Action OnInit;


    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override WrapperCustomEventPriorityT<bool> OnUpdateStatus => _buttonSelect.OnUpdateStatusSelect;

    public override bool GetStatusBool()
    {
        return _buttonSelect.StatusSelect;
    }
}
