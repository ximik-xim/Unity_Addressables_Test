using System;
using System.Collections.Generic;
using UnityEngine;

public class LogicLoadStorageBlockSceneIn_SDStringStorage : AbsLogicLoadInStorageBlockScene
{
    [SerializeField]
    private SD_AbsStringStorage _absStringStorage;

    [SerializeField]
    private GetDataSO_SD_KeyStorageStringVariable _keyData;
    
    public override event Action OnInit;
    public override bool IsInit => _isInit;
    private bool _isInit = false;

    private void Awake()
    {
        if (_absStringStorage.IsInit == false) 
        {
            _absStringStorage.OnInit += OnInitStorage;
        }

        CheckInit();
    }

    private void OnInitStorage()
    {
        if (_absStringStorage.IsInit == true) 
        {
            _absStringStorage.OnInit -= OnInitStorage;
            
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_absStringStorage.IsInit == true)
        {
            _isInit = true;
            OnInit?.Invoke();
        }
    }


    //тут как callback это точно 
    public override JsDataStorageBlockScene GetSaveDataJS()
    {
        var jsData= _absStringStorage.GetData(_keyData.GetData());
        
        if (jsData == string.Empty || jsData == "")
        {
            return new JsDataStorageBlockScene(new List<AbsKeyData<string, bool>>());
        }
        else
        {
            JsDataStorageBlockScene listData = JsonUtility.FromJson<JsDataStorageBlockScene>(jsData);

            return listData;
        }
    }
}
