using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_TSG_KeyStorageTask : AbsIdentifierAndData<IndifNameSO_TSG_KeyStorageTask, string, TSG_KeyStorageTask>
{

 [SerializeField] 
 private TSG_KeyStorageTask _dataKey;


 public override TSG_KeyStorageTask GetKey()
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
  _dataKey = JsonUtility.FromJson<TSG_KeyStorageTask>(json);
 }
#endif
}
