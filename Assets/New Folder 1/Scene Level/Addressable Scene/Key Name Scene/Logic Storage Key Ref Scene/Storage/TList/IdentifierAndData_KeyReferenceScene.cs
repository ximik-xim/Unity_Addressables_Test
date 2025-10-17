using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyReferenceScene : AbsIdentifierAndData<IndifNameSO_KeyReferenceScene, string, KeyReferenceScene>
{

 [SerializeField] 
 private KeyReferenceScene _dataKey;

 public override KeyReferenceScene GetKey()
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
 _dataKey = JsonUtility.FromJson<KeyReferenceScene>(json);
 }
#endif
}
