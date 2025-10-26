using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataAudioMixerVariable : AbsIdentifierAndData<AudioMixerVariableIndifNameSO, string, CS_KeyAudioMixerVarible>
{

    [SerializeField]
    private CS_KeyAudioMixerVarible _dataKey;


    public override CS_KeyAudioMixerVarible GetKey()
    {
        return _dataKey;
    }
    
#if UNITY_EDITOR
    public override string GetJsonSaveData()
    {
        return JsonUtility.ToJson(_dataKey);
    }

    public override void SetJsonData(string json)
    {
        _dataKey = JsonUtility.FromJson<CS_KeyAudioMixerVarible>(json);
    }
#endif
}