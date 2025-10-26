using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CS_KeyAudioMixerVarible 
{
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
