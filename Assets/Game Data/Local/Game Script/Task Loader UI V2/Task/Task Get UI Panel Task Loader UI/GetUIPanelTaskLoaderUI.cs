
using System;
using UnityEngine;

/// <summary>
/// Нужен что бы
/// 1) получить панель Panel Task Loader UI
/// 2) добавить её Game Object в хран Key Storage GM (по указ. ключу)
/// 3) уст этому GM нового родителя(т.к Main Camera будет со сцены Dont Destroy братца)
/// </summary>
public class GetUIPanelTaskLoaderUI : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private GetDKOPatch _getDkoPatch;

    [SerializeField]
    private StorageKeyAndGM _storageGM;

    [SerializeField]
    private GameObject _targetParent;

    [SerializeField]
    private GetDataSO_StorageKeyGM _keyStorageGM;
    
    [SerializeField]
    private StorageTypeLog _storageTypeLog;
    public event Action<AbsKeyData<KeyTaskLoaderTypeLog, string>> OnAddLogData;
    
    private void Awake()
    {
        if (_getDkoPatch.Init == false)
        {
            _getDkoPatch.OnInit += OnInitGetDkoPatch;
        }

        if (_storageGM.IsInit == false)
        {
            _storageGM.OnInit += OnInitStorageGM;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_getDkoPatch.Init == true)
        {
            _getDkoPatch.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
        
    }
    
    private void OnInitStorageGM()
    {
        if (_getDkoPatch.Init == true)
        {
            _storageGM.OnInit -= OnInitStorageGM;
            CheckInit();
        }
        
    }
    
    private void CheckInit()
    {
        if (_getDkoPatch.Init == true && _storageGM.IsInit == true) 
        {
            InitData();
        }
    }

    private void InitData()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public void StartLogic()
    {
        DebugLog(_storageTypeLog.GetKeyDefaultLog(), "Получение GM UI панели Task Loader UI через DKO");
        GameObject uiPanel = _getDkoPatch.GetDKO<DKODataInfoT<GameObject>>().Data;

        DebugLog(_storageTypeLog.GetKeyDefaultLog(), $"Добавление GM панели в Storage GM по ключу {_keyStorageGM.GetData().GetKey()}");
        _storageGM.AddGM(_keyStorageGM.GetData(), uiPanel);

        DebugLog(_storageTypeLog.GetKeyDefaultLog(), "Установка нового родителя у панели");
        uiPanel.transform.parent = _targetParent.transform;
        uiPanel.transform.localPosition = Vector3.zero;
        uiPanel.transform.localScale = Vector3.one;
    }
    
    private void DebugLog(KeyTaskLoaderTypeLog keyLog, string text)
    {
        var logData = new AbsKeyData<KeyTaskLoaderTypeLog, string>(keyLog, text);
        OnAddLogData?.Invoke(logData);
    }
}
