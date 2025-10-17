using System;
using UnityEngine;


/// <summary>
/// Обобщение для удобного создания обертки, для получения обькета(Data) с учетом инициализации 
/// </summary>
//вынести в General
public abstract class AbsInitData<Data> : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    public abstract Data GetData();
}
