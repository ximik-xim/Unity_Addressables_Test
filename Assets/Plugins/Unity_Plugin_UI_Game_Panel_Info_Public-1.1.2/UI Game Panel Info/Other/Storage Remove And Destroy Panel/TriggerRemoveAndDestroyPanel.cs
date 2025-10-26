using System;
using UnityEngine;

public class TriggerRemoveAndDestroyPanel : MonoBehaviour
{
  [SerializeField] 
  private StorageUIGamePanelRemoveAndDestroyPanel _storageUIGamePanelRemoveAnd;
  
  [SerializeField] 
  private GetDataSO_KeyUIGamePanelInfo _keyPanel;
  
  private void OnDestroy()
  {
    _storageUIGamePanelRemoveAnd.StartAction(_keyPanel.GetData());
  }
}
