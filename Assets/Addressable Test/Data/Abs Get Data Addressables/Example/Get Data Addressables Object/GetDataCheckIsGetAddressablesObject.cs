using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// Нужен для проверки, можно ли взять этот обьект(к примеру с локального хран)
/// </summary>
[System.Serializable]
public class GetDataCheckIsGetAddressablesObject : AbsCallbackGetDataCheckIsGet<AbsCallbackGetDataAddressables, object, AbsBoolIsGetAddressablesObject>
{

}
