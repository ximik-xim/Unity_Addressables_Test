using TListPlugin;
using UnityEngine;
[System.Serializable]
public class KeyIndifNameSOData_MBS_DKO : AbsIdentifierAndData<MessengerDKOBetweenSceneIndifNameSO,string,MessengerBetweenSceneKeyDKO>
{
    [SerializeField] 
    private MessengerBetweenSceneKeyDKO dataKeyDKO;

    public override MessengerBetweenSceneKeyDKO GetKey()
    {
        return dataKeyDKO;
    }
    
#if UNITY_EDITOR
    public override string GetJsonSaveData()
    {
        return JsonUtility.ToJson(dataKeyDKO);
    }

    public override void SetJsonData(string json)
    {
        dataKeyDKO = JsonUtility.FromJson<MessengerBetweenSceneKeyDKO>(json);
    }
#endif
}
