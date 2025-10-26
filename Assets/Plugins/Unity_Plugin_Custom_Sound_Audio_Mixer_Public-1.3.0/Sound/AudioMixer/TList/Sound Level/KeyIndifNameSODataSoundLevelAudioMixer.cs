using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataSoundLevelAudioMixer : AbsIdentifierAndData<SoundLevelAudioMixerIndifNameSO, string, CS_KeySoundLevelAudioMixer>
{
    [SerializeField]
    private CS_KeySoundLevelAudioMixer _dataKey;


    public override CS_KeySoundLevelAudioMixer GetKey()
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
        _dataKey = JsonUtility.FromJson<CS_KeySoundLevelAudioMixer>(json);
    }
#endif
}
