using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CS_TaskDataMuteSoundLevel 
{
    private string _text;

    public CS_TaskDataMuteSoundLevel(string text)
    {
        _text = text;   
    }

    public string GetInfo()
    {
        return _text;
    }
}
