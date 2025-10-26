using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataTaskMuteSoundLevel : AbsIdentifierAndData<TaskMuteSoundLevelIndifNameSO, string, CS_KeyTaskDataMuteSoundLevel>
{

    [SerializeField]
    private CS_KeyTaskDataMuteSoundLevel _dataKey;


    public override CS_KeyTaskDataMuteSoundLevel GetKey()
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
        _dataKey = JsonUtility.FromJson<CS_KeyTaskDataMuteSoundLevel>(json);
    }
#endif
}