using System;
using UnityEngine;


    /// <summary>
    /// Нужен для опр. действий, в случае если не удалось загр ресурс с 1 попытки(продолжать попытки или нет)
    /// </summary>
public abstract class LogicErrorCallbackRequestAddressables : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    
    public abstract event Action OnUpdateData;
    public abstract bool IsContinue { get; }


    public abstract void OnAddError();
    public abstract void OnRemoveAllError();
}
