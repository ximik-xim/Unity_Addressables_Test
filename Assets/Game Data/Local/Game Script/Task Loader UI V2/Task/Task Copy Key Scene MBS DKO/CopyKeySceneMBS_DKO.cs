using System;
using UnityEngine;

/// <summary>
/// Нужен, что бы скапировать ключи
/// из Scene MBS DKO
/// в Dont Destroy MBS DKO
/// </summary>
public class CopyKeySceneMBS_DKO : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private FindMBS_DKO_DontDestroy _findMbsDkoDontDestroy;

    [SerializeField]
    private SceneMBS_DKO _sceneMbsDko;
    
    [SerializeField]
    private StorageTypeLog _storageTypeLog;

    /// <summary>
    /// Удалять ли скопированные ключ, при уничтожении этой Task
    /// </summary>
    [SerializeField]
    private bool _deletedCopyKeyDestroy = false;
    
    public event Action<AbsKeyData<KeyTaskLoaderTypeLog, string>> OnAddLogData;
    
    private void Awake()
    {
        if (_findMbsDkoDontDestroy.Init == false)
        {
            _findMbsDkoDontDestroy.OnInit += OnInitFindMbsDkoDontDestroy;
        }

        if (_sceneMbsDko.IsInit == false)
        {
            _sceneMbsDko.OnInit += OnInitSceneMbsDko;
        }
        
        CheckInit();
    }
    
    private void OnInitFindMbsDkoDontDestroy()
    {
        if (_findMbsDkoDontDestroy.Init == true)
        {
            _findMbsDkoDontDestroy.OnInit -= OnInitFindMbsDkoDontDestroy;
            CheckInit();
        }
        
    }
    
    private void OnInitSceneMbsDko()
    {
        if (_findMbsDkoDontDestroy.Init == true)
        {
            _sceneMbsDko.OnInit -= OnInitSceneMbsDko;
            CheckInit();
        }
        
    }
    
    private void CheckInit()
    {
        if (_findMbsDkoDontDestroy.Init == true && _sceneMbsDko.IsInit == true) 
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
        DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Копирую ключи из Scene MBS DKO в Dont Destroy MBS DKO");
        _findMbsDkoDontDestroy.GetDontDestroyMbsDko.CopyDataTargetStorage(_sceneMbsDko);
    }
    
    private void DebugLog(KeyTaskLoaderTypeLog keyLog, string text)
    {
        var logData = new AbsKeyData<KeyTaskLoaderTypeLog, string>(keyLog, text);
        OnAddLogData?.Invoke(logData);
    }

    private void OnDestroy()
    {
        if (_deletedCopyKeyDestroy == true)
        {
            if (_findMbsDkoDontDestroy.Init == true && _sceneMbsDko.IsInit == true)
            {
                DebugLog(_storageTypeLog.GetKeyDefaultLog(), "- Удаляю ранее скопированные ключи из Scene MBS DKO в Dont Destroy MBS DKO");
            
                var copyData = _sceneMbsDko.GetCopyData();

                foreach (var VARIABLE in copyData)
                {
                    _findMbsDkoDontDestroy.GetDontDestroyMbsDko.RemoveDKO(VARIABLE.Key);
                }
            }
        }
    }
}
