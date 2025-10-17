using System.Collections.Generic;
using UnityEngine;

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
