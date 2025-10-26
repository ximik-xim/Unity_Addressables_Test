using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_StorageKeyGM : AbsIdentifierAndData<IndifNameSO_StorageKeyGM, string, StorageKeyGM>
{

 [SerializeField] 
 private StorageKeyGM _dataKey;


 public override StorageKeyGM GetKey()
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
  _dataKey = JsonUtility.FromJson<StorageKeyGM>(json);
 }
#endif
}
