using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TList_ExampleKey 
{
    [SerializeField]
    private string key;

    public string GetKey()
    {
        return key;
    }
}
