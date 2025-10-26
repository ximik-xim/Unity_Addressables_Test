using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ListActionGmActiveAndDisactive : MonoBehaviour
{
    /// <summary>
    /// Определяем, по какому списку действий пройдемся
    /// или с начало по списку дизактивации обьектов, а потом по списку активации обьектов
    /// или с начало по списку активации обьектов, а потом по списку дизактивации обьектов
    /// </summary>
    [SerializeField] 
    private TypeStartActionGmActiveAndDisactive _typeStart;
    
    [SerializeField] 
    private List<GameObject> _gmActive = new List<GameObject>();

    [SerializeField] 
    private List<GameObject> _gmDisactive = new List<GameObject>();

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
                VARIABLE.gameObject.SetActive(false);
            }

            foreach (var VARIABLE in _gmActive)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var VARIABLE in _gmActive)
            {
                VARIABLE.gameObject.SetActive(true);
            }
            
            foreach (var VARIABLE in _gmDisactive)
            {
                VARIABLE.gameObject.SetActive(false);
            }
        }

        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();

    }
    
    
}

public enum TypeStartActionGmActiveAndDisactive
{
    StartDisactiveAndActive,
    StartActiveAndDisactive
}