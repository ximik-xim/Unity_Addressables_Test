using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsLogicLoadInStorageBlockScene : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }

    public abstract JsDataStorageBlockScene GetSaveDataJS();
}
