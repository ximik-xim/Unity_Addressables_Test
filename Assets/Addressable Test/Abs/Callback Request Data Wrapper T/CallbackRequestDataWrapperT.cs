using UnityEngine;

/// <summary>
/// Обертка нужна, для получения данных через callback(с учетом доп логики)
/// </summary>
public class CallbackRequestDataWrapperT<T> : AbsServerRequestDataWrapper<T>
{
    public CallbackRequestDataWrapperT(int id) : base(id)
    {
    }
}
