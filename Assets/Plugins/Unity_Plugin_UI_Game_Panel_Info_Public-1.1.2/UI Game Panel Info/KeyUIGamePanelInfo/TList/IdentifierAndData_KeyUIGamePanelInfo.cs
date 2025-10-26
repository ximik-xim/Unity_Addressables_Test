using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyUIGamePanelInfo : AbsIdentifierAndData<IndifNameSO_KeyUIGamePanelInfo, string, KeyUIGamePanelInfo>
{

 [SerializeField] 
 private KeyUIGamePanelInfo _dataKey;


 public override KeyUIGamePanelInfo GetKey()
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
  _dataKey = JsonUtility.FromJson<KeyUIGamePanelInfo>(json);
 }
#endif
}
