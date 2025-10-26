using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранилище с ключами основных типов логов
/// Нужен для упрощения работы
/// (что бы каждый раз не указывать ключи у всех Task)
/// </summary>
public class StorageTypeLog : MonoBehaviour
{
    [SerializeField] 
    private GetDataSO_KeyTaskLoaderTypeLog _keyDefaultTypeLog;
    [SerializeField] 
    private GetDataSO_KeyTaskLoaderTypeLog _keyWarningTypeLog;
    [SerializeField] 
    private GetDataSO_KeyTaskLoaderTypeLog _keyFatalErrorTypeLog;

    public KeyTaskLoaderTypeLog GetKeyDefaultLog()
    {
        return _keyDefaultTypeLog.GetData();
    }
    
    public KeyTaskLoaderTypeLog GetKeyWarningLog()
    {
        return _keyWarningTypeLog.GetData();
    }
    
    public KeyTaskLoaderTypeLog GetKeyErrorLog()
    {
        return _keyFatalErrorTypeLog.GetData();
    }
}
