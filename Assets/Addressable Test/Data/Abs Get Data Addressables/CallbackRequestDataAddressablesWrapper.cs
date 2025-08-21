using UnityEngine;

/// <summary>
/// Обертка нужна, для получения данных из Addressables через callback(с учетом доп логики)
/// </summary>
/// <typeparam name="T"></typeparam>
public class CallbackRequestDataAddressablesWrapper<T> : AbsServerRequestDataWrapper<T>
{
    public CallbackRequestDataAddressablesWrapper(int id) : base(id)
    {
    }
}
