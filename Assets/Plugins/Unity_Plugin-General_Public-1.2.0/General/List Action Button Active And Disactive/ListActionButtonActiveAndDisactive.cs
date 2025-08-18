using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListActionButtonActiveAndDisactive : MonoBehaviour
{
    [SerializeField] 
    private TypeStartActionGmActiveAndDisactive _typeStart;
    
    [SerializeField] 
    private List<Button> _gmActive = new List<Button>();

    [SerializeField] 
    private List<Button> _gmDisactive = new List<Button>();

    public event Action OnCompletedLogic;
    public bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = true;

    public void StartAction()
    {
        _isCompletedLogic = false;
        
        if (_typeStart == TypeStartActionGmActiveAndDisactive.StartDisactiveAndActive)
        {
            foreach (var VARIABLE in _gmDisactive)
            {
                VARIABLE.interactable = false;
            }

            foreach (var VARIABLE in _gmActive)
            {
                VARIABLE.interactable = true;
            }
        }
        else
        {
            foreach (var VARIABLE in _gmActive)
            {
                VARIABLE.interactable = true;
            }
            
            foreach (var VARIABLE in _gmDisactive)
            {
                VARIABLE.interactable = false;
            }
        }

        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();

    }
}
