using System;
using System.Collections.Generic;
using UnityEngine;

public class ListActionGmSetHeightAndWidth : MonoBehaviour
{
    [SerializeField] 
    private bool _isActiveAwake;

    [SerializeField]
    private bool _setHeight = true;
    [SerializeField]
    private float _height = 0f;
    
    [SerializeField]
    private bool _setWidth = true;
    [SerializeField]
    private float _width = 0f;
    
    
    [SerializeField] 
    private List<RectTransform> _gm = new List<RectTransform>();
    
    public event Action OnCompletedLogic;
    public bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = true;
    
    private void Awake()
    {
        if (_isActiveAwake == true)
        {
            StartAction();
        }
    }

    public void StartAction()
    {
        _isCompletedLogic = false;

        foreach (var VARIABLE in _gm)
        {
            if (_setHeight == true) 
            {
                VARIABLE.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
            }
            
            if (_setWidth == true)
            {
                VARIABLE.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _width);
            }
        }

        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }
    
    
    public void SetHeightAndWidth(List<GameObject> targetGm)
    {
        foreach (var VARIABLE in _gm)
        {
            if (_setHeight == true) 
            {
                VARIABLE.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
            }
            
            if (_setWidth == true)
            {
                VARIABLE.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _width);
            }
        }
    } 

    public IReadOnlyList<RectTransform> GetListGm()
    {
        return _gm;
    }
}
