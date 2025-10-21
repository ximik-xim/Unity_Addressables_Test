using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyTaskLoaderTypeLog : AbsIdentifierAndData<IndifNameSO_KeyTaskLoaderTypeLog, string, KeyTaskLoaderTypeLog>
{

 [SerializeField] 
 private KeyTaskLoaderTypeLog _dataKey;


 public override KeyTaskLoaderTypeLog GetKey()
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
  _dataKey = JsonUtility.FromJson<KeyTaskLoaderTypeLog>(json);
 }
#endif
}
