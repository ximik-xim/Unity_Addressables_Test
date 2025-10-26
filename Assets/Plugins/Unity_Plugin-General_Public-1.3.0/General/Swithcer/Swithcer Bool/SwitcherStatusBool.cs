using System.Collections.Generic;
using UnityEngine;

public class SwitcherStatusBool : MonoBehaviour
{
    [SerializeField]
    private IGetBoolStatusMono _statusDataMono;
    [SerializeField] 
    private bool _checkAwake;
    [SerializeField]
    private bool _checkOnEnable;
    [SerializeField]
    private bool _checkEvent;
    
    /// <summary>
    /// В этом списке все обьекты будут включены, если SDK доступен, если нет, то будут выключены
    /// </summary>
    [SerializeField] 
    private List<GameObject> _connectedEnableGM = new List<GameObject>();

    [SerializeField] 
    private List<GameObject> _connectedDisableGM = new List<GameObject>();

    private IGetBoolStatus _statusData;
    
    private void Awake()
    {
        
        if (_statusDataMono.IsInit == false)
        {
            _statusDataMono.OnInit += OnInitGetData;  
            return;
        }

        InitGetData();

    }

    private void OnInitGetData()
    {
        _statusDataMono.OnInit -= OnInitGetData;
        InitGetData();
    }


    private void InitGetData()
    {
        _statusData = _statusDataMono.GetDataStatus();

     
            
            if (_checkEvent == true)
            {
                _statusData.OnUpdateStatus += CheckStatusData;
            }

            if (_checkAwake == true)
            {
                CheckStatusData();
            }
            
    }
    
    
    private void OnEnable()
    {
        if (_checkOnEnable == true)
        {
            if (_statusData != null)  
            {
                CheckStatusData();
            }
        }
        
    }


    private void CheckStatusData()
    {
  
        if (_statusData.GetStatus == true)
        {
            foreach (var VARIABLE in _connectedDisableGM)
            {
                VARIABLE.gameObject.SetActive(false);
            }

            foreach (var VARIABLE in _connectedEnableGM)
            {
                VARIABLE.gameObject.SetActive(true);
            }

        }
        else if (_statusData.GetStatus == false) 
        {
            foreach (var VARIABLE in _connectedEnableGM)
            {
                VARIABLE.gameObject.SetActive(false);
            }

            foreach (var VARIABLE in _connectedDisableGM)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        if (_statusData != null)
        {
            _statusData.OnUpdateStatus -= CheckStatusData;    
        }
    }
}
