using System;
using System.Collections.Generic;
using UnityEngine;

public class ListActionGmSetPosition : MonoBehaviour
{
    [SerializeField] 
    private bool _isActiveAwake;
    
    /// <summary>
    /// Если false, то будут исп. локальные коорд.
    /// Если True, то будут исп. глобальные коорд.
    /// </summary>
    [SerializeField] 
    private bool _isSetGlobalPosition = false;
    
    [SerializeField] 
    private Vector3 _setPosition = Vector3.zero;

    [SerializeField]
    private bool _setPosX = true;
    
    [SerializeField]
    private bool _setPosY = true;
    
    [SerializeField]
    private bool _setPosZ = true;
    
    
    [SerializeField] 
    private List<GameObject> _gm = new List<GameObject>();
    
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
            Vector3 targetPos;
            
            if (_isSetGlobalPosition == true)
            {
                targetPos = FilterPos(VARIABLE.gameObject.transform.position);
                
                VARIABLE.gameObject.transform.position = targetPos;    
            }
            else
            {
                targetPos = FilterPos(VARIABLE.gameObject.transform.localPosition);
                VARIABLE.gameObject.transform.localPosition = targetPos;
            }
        }

        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }

    private Vector3 FilterPos(Vector3 targetPos)
    {
        if (_setPosX == true)
        {
            targetPos = new Vector3(_setPosition.x, targetPos.y, targetPos.z);
        }
            
        if (_setPosY == true)
        {
            targetPos = new Vector3(targetPos.x, _setPosition.y, targetPos.z);
        }
            
        if (_setPosZ == true)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, _setPosition.z);
        }

        return targetPos;
    }
    
    public void SetPos(List<GameObject> targetGm)
    {
        foreach (var VARIABLE in targetGm)
        {
            Vector3 targetPos;
            
            if (_isSetGlobalPosition == true)
            {
                targetPos = FilterPos(VARIABLE.gameObject.transform.position);
                
                VARIABLE.gameObject.transform.position = targetPos;    
            }
            else
            {
                targetPos = FilterPos(VARIABLE.gameObject.transform.localPosition);
                VARIABLE.gameObject.transform.localPosition = targetPos;
            }
        }
    } 

    public IReadOnlyList<GameObject> GetListGm()
    {
        return _gm;
    }
}
