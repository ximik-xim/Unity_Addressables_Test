using UnityEngine;

public class StorageUIGamePanelRemoveAndDestroyPanel : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _patchStorageGamePanelInfo;

    public void StartAction(KeyUIGamePanelInfo key)
    {
        var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
        UIGamePanelInfoStorage taskInfo = DKOData.Data;
        
        var panelInfo = taskInfo.GetPanel(key);
        
        taskInfo.RemovePanel(key);
        Destroy(panelInfo.GetParent());
    }
}
