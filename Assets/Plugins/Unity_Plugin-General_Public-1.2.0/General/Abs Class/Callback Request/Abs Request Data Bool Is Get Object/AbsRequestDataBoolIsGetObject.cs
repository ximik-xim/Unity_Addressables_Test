using System;
using UnityEngine;

/// <summary>
/// Нужна, что бы определить, можно ли взять обьект
/// (к примеру, хотим взять обьект из локального хран, и дел. запрос, а можно ли)
/// </summary>
public abstract class AbsRequestDataBoolIsGetObject<ArgData> : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;

    /// <summary>
    /// Определяем, можно ли этот взять
    /// </summary>
    /// <param name="Data"></param>
    public abstract GetServerRequestData<bool> IsGet(ArgData data);
}
