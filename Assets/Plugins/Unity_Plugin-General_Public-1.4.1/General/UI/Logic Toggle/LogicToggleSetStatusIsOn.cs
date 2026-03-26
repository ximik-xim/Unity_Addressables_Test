using System;
using UnityEngine;
using UnityEngine.UI;

public class LogicToggleSetStatusIsOn : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;

    [SerializeField]
    private bool _statusToggleIsOn = true;
    
    
    [SerializeField]
    private bool _setAwake;
    
    [SerializeField]
    private bool _setStart;
    
    [SerializeField]
    private bool _setOnEnable;
    
    [SerializeField]
    private bool _setOnDisable;
    
    [SerializeField]
    private bool _setOnDestroy;


    private void Awake()
    {
        if (_setAwake == true)
        {
            SetIsOn();
        }
    }

    private void Start()
    {
        if (_setStart == true)
        {
            SetIsOn();
        }
    }

    private void OnEnable()
    {
        if (_setOnEnable == true)
        {
            SetIsOn();
        }
    }

    private void OnDisable()
    {
        if (_setOnDisable == true)
        {
            SetIsOn();
        }
    }

    private void OnDestroy()
    {
        if (_setOnDestroy == true)
        {
            SetIsOn();
        }
    }

    public void SetIsOn()
    {
        _toggle.isOn = _statusToggleIsOn;
        _toggle.onValueChanged.Invoke(_toggle.isOn);
    }
}
