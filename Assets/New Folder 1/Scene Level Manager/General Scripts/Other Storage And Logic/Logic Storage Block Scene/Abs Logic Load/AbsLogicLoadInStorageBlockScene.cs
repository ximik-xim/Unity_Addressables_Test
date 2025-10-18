using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика для загрузки данных в виде списка сцен с их статусом(заблокир или нет)
/// </summary>
public abstract class AbsLogicLoadInStorageBlockScene : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }

    public abstract JsDataStorageBlockScene GetSaveDataJS();
}
