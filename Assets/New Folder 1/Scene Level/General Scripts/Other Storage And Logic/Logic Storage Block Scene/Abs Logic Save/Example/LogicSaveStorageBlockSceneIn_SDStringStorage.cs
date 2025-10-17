using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика для сохранения данных в виде списка сцен с их статусом(заблокир или нет)
/// Через SD_AbsStringStorage
/// </summary>
public class LogicSaveStorageBlockSceneIn_SDStringStorage : AbsLogicSaveInStorageBlockScene
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
    
    public override void SetSaveDataJS(JsDataStorageBlockScene data)
    {
        string jsData = JsonUtility.ToJson(data);
        _absStringStorage.SetData(_keyData.GetData(), jsData);
    }
    
    public override void SaveDataJS()
    {
        _absStringStorage.SaveData(new TaskInfo("Сохранение списка с заблокир. сценами"));
    }
    
}
