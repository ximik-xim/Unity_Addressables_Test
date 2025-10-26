using TListPlugin;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataDKODataKey : AbsIdentifierAndData<DKOIndifNameSO,string,DKODataKey>
{
    [SerializeField] 
    private DKODataKey _dataKey;
    
    public override DKODataKey GetKey()
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
        _dataKey = JsonUtility.FromJson<DKODataKey>(json);
    }
#endif
}
