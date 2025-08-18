using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_TList_Example : AbsIdentifierAndData<IndifNameSO_TList_Example, string, TList_ExampleKey>
{

 [SerializeField] 
 private TList_ExampleKey _dataKey;

 public override TList_ExampleKey GetKey()
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
  _dataKey = JsonUtility.FromJson<TList_ExampleKey>(json);
 }
#endif
}
