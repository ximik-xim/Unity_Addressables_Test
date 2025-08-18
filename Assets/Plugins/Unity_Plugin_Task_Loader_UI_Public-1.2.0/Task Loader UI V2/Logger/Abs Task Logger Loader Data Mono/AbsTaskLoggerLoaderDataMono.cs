using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsTaskLoggerLoaderDataMono : AbsTaskLoaderDataMono
{
    [SerializeField] 
    protected LoggerData _storageLog;
    
    [SerializeField] 
    protected StorageTypeLog _storageTypeLog;
}
