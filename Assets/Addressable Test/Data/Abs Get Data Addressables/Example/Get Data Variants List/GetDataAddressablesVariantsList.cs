using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


/// <summary>
/// Содержит список вариантов, откудова можно взять обьект.
/// (при ERROR от сервера, будет переключаться на след. вариант)
/// Будет поочереди перебирать все варианты, пока кто то не вернет статус OK, или пока не закончаться варианты
/// </summary>
public class GetDataAddressablesVariantsList : AbsCallbackGetDataVariantsList<object>
{
    
}
//AbsCallbackGetData<object>
    //AbsCallbackGetDataAddressables