using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_NameScene : AbsIdentifierAndData<IndifNameSO_NameScene, string, KeyNameScene>
{

 [SerializeField] 
 private KeyNameScene _dataKey;

 public override KeyNameScene GetKey()
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
 _dataKey = JsonUtility.FromJson<KeyNameScene>(json);
 }
#endif
}
