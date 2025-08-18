using System;
using System.Collections.Generic;
using UnityEngine;

public class SetParentStorageKeyAndGM : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _patchStorageKeyAndGM;

    [SerializeField] 
    private ListActionGmSetParent _listActionGm;

    [SerializeField]
    private bool _startAwake = false;

    [SerializeField]
    private GetDataSO_StorageKeyGM _keyGM;
 
    private void Awake()
    {
        if (_patchStorageKeyAndGM.Init == false)
        {
            _patchStorageKeyAndGM.OnInit += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _patchStorageKeyAndGM.OnInit -= OnInit;
        Init();
    }

    private void Init()
    {
        if (_startAwake == true) 
        {
            StartAction();
        }
    }


    public void StartAction()
    {
        var DKOData = (DKODataInfoT<StorageKeyAndGM>)_patchStorageKeyAndGM.GetDKO();
        StorageKeyAndGM taskInfo = DKOData.Data;

        var parent = taskInfo.GetGM(_keyGM.GetData());

        _listActionGm.StartAction(parent);
    }
    
    
    
}
