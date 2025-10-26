using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен для упрощения, что бы у наследников уже были ссылки на
/// на логику логера  
/// и на хранилеще с типами логов
/// </summary>
public abstract class AbsTaskLoggerLoaderDataMono : AbsTaskLoaderDataMono
{
    [SerializeField] 
    protected LoggerData _storageLog;
    
    [SerializeField] 
    protected StorageTypeLog _storageTypeLog;
}
