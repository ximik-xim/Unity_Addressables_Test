using System;
using UnityEngine;

/// <summary>
/// Счетчик ошибок подрят, при запросе к Addressables
/// </summary>
public class CounterErrorRequestAddressables : LogicErrorCallbackRequestAddressables
{
    public override bool IsInit => true;
    public override event Action OnInit;
    
    public override event Action OnUpdateData;
    public override bool IsContinue => _isContinue;
    [SerializeField]
    private bool _isContinue = true;

    private int _currentCoutn = 0;
    [SerializeField] 
    private int _targetCount = 3;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override void OnAddError()
    {
        _currentCoutn++;

        if (_currentCoutn >= _targetCount)
        {
            if (_isContinue == true)
            {
                _isContinue = false;
                OnUpdateData?.Invoke();
            }
            
        }
    }

    public override void OnRemoveAllError()
    {
        RemoveBlock();
    }

    private void RemoveBlock()
    {
        _currentCoutn = 0;
        
        if (_isContinue == false)
        {
            _isContinue = true;
            OnUpdateData?.Invoke();
        }
    }
}
