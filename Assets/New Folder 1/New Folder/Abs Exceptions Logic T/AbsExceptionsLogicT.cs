using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстракция для создания списка исключений
/// На случай, если по ключу нужно вернуть не стандартные данные
/// </summary>
public abstract class AbsExceptionsLogicT<Key,Data> : MonoBehaviour
{
    public abstract event Action OnInit;
    public abstract bool IsInit { get; }
    public abstract List<AbsKeyData<Key, Data>> GetListExceptions();
}
