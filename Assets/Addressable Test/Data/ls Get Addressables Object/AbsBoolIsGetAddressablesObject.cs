using System;
using UnityEngine;

/// <summary>
/// Нужна, что бы определить, можно ли взять обьект
/// (к примеру, хотим взять обьект из локального хран, и дел. запрос, а можно ли)
/// </summary>
public abstract class AbsBoolIsGetAddressablesObject : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    /// <summary>
    /// Определяем, можно ли этот взять
    /// </summary>
    /// <param name="Data"></param>
    public abstract bool IsGet(object data);
}
