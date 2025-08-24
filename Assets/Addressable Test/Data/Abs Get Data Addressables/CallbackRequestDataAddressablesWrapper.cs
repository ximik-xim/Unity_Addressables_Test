using UnityEngine;

/// <summary>
/// Обертка нужна, для получения данных из Addressables через callback(с учетом доп логики)
/// </summary>
public class CallbackRequestDataAddressablesWrapper<T> : AbsServerRequestDataWrapper<T>
{
    public CallbackRequestDataAddressablesWrapper(int id) : base(id)
    {
    }
}
