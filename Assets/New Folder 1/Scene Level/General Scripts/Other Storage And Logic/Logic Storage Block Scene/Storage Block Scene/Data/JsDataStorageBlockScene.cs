using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные об статусе сцен
/// (сцен - статус(заблокир/разблокир))
/// Сделал в виде отдел. класса, что бы сохранять и загружать в виде JS
/// </summary>
[System.Serializable]
public class JsDataStorageBlockScene
{
    public JsDataStorageBlockScene(List<AbsKeyData<string, bool>> listData)
    {
        ListData = listData;
    }

    [SerializeField]
    public List<AbsKeyData<string, bool>> ListData;

}
