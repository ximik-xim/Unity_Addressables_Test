using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Это обертка нужна, что бы сделать переотправку запроса несколько раз(в случ. ошибки)
/// </summary>
[System.Serializable]
public class GetDataAddressablesErrorContinueT : AbsCallbackGetDataErrorContinueT<AbsCallbackGetDataTAddressables, object>
{
    
}
