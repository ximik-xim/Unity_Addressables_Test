using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataSnaphotsSetAudioMixer : AbsIdentifierAndData<SnaphotsSetAudioMixerIndifNameSO, string, CS_KeySnaphotsSetAudioMixer>
{

    [SerializeField]
    private CS_KeySnaphotsSetAudioMixer _dataKey;


    public override CS_KeySnaphotsSetAudioMixer GetKey()
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
        _dataKey = JsonUtility.FromJson<CS_KeySnaphotsSetAudioMixer>(json);
    }
#endif
}