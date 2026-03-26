using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyDropdown : AbsIdentifierAndData<IndifNameSO_KeyDropdown, string, KeyDropdown>
{

 [SerializeField] 
 private KeyDropdown _dataKey;

 public override KeyDropdown GetKey()
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
 _dataKey = JsonUtility.FromJson<KeyDropdown>(json);
 }
#endif
}
