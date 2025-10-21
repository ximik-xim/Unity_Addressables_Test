using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyTaskLoaderTypeLog
{
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
