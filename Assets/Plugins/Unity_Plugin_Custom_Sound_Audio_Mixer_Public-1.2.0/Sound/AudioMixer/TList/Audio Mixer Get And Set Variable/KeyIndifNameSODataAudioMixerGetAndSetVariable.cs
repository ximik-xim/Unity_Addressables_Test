using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataAudioMixerGetAndSetVariable : AbsIdentifierAndData<AudioMixerGetAndSetVariableIndifNameSO, string, CS_KeyAudioMixerGetAndSetVariable>
{

    [SerializeField]
    private CS_KeyAudioMixerGetAndSetVariable _dataKey;


    public override CS_KeyAudioMixerGetAndSetVariable GetKey()
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
        _dataKey = JsonUtility.FromJson<CS_KeyAudioMixerGetAndSetVariable>(json);
    }
#endif
}
