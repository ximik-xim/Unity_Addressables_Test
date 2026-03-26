using System;
using System.Collections.Generic;
using UnityEngine;

public class SetParentStorageKeyAndGM : MonoBehaviour
{
    /// <summary>
    /// Уничтожать ли GM при Destroy этого скрипта
    /// </summary>
    [SerializeField]
    private bool _isGmDestroy;
    
    [SerializeField]
    private GetDKOPatch _patchStorageKeyAndGM;

    [SerializeField]
    private ListActionGmSetParent _setParent;

    [SerializeField]
    private bool _startAwake = false;

    [SerializeField]
    private GetDataSO_StorageKeyGM _keyGetParent;
 
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
        StorageKeyAndGM storageKeyAndGm = _patchStorageKeyAndGM.GetDKO<DKODataInfoT<StorageKeyAndGM>>().Data;
        GameObject parent = storageKeyAndGm.GetGM(_keyGetParent.GetData());

        _setParent.StartAction(parent);
    }
    
    private void OnDestroy()
    {
        if (_isGmDestroy == true)
        {
            foreach (var VARIABLE in _setParent.GetListGm())
            {
                Destroy(VARIABLE);
            }
        }
    }
    
}
