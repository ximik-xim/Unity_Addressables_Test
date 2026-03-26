
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужна, что бы можно было получить список ключей для Dropdown из разных мест
/// </summary>
public abstract class AbsGetListKeyDropdown : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;


    public abstract List<KeyDropdown> GetListKeyDropdown();
}
