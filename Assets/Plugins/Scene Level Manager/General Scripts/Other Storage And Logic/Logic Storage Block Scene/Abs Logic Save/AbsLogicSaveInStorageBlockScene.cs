using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика для сохранения данных в виде списка сцен с их статусом(заблокир или нет)
/// </summary>
public abstract class AbsLogicSaveInStorageBlockScene : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    
    public abstract void SaveDataJS();

    public abstract void SetSaveDataJS(JsDataStorageBlockScene data);
}
