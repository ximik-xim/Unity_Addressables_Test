using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_NameSceneAndKeyString : AbsIdentifierAndData<IndifNameSO_NameSceneAndKeyString, string, NameSceneAndKeyString>
{

 [SerializeField] 
 private NameSceneAndKeyString _dataKey;

 public override NameSceneAndKeyString GetKey()
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
 _dataKey = JsonUtility.FromJson<NameSceneAndKeyString>(json);
 }
#endif
}
