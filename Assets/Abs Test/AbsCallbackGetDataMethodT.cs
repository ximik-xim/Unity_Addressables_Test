using System;
using UnityEngine;

/// <summary>
/// Некий обобщенный метод для получения указ типа данных через callback(который принимает указанный тип аргумента(может класс, может просто стринга и т.д)
/// </summary>
//вынести в General
public abstract class AbsCallbackGetDataMethodT<ArgData> : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    public abstract GetServerRequestData<T> GetData<T>(ArgData data);
}
