using UnityEngine;
using UnityEngine.UI;

public class ToggleDevelopPanelOpenAndCloseStorageTaskLoaderUI : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;

    private StorageTaskLoaderUI _storageTaskLoaderPanelUI;

    [SerializeField]
    private bool _isOpen;
   
    [SerializeField]
    private GetDKOPatch _patchStorageTaskLoaderPanelUI;
    private void Awake()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == false)
        {
            _patchStorageTaskLoaderPanelUI.OnInit += OnInitGetDkoPatch;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == true)
        {
            _patchStorageTaskLoaderPanelUI.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == true)
        {
            _storageTaskLoaderPanelUI = _patchStorageTaskLoaderPanelUI.GetDKO<DKODataInfoT<StorageTaskLoaderUI>>().Data;

            _storageTaskLoaderPanelUI.OnUpdateStatusOpen += OnUpdateStatusOpen;
            OnUpdateStatusOpen();
            
            _toggle.onValueChanged.AddListener(ToggleClick);
        }
    }

    private void OnUpdateStatusOpen()
    {
        if (_toggle.isOn != _storageTaskLoaderPanelUI.IsOpen)
        {
            _toggle.SetIsOnWithoutNotify(_storageTaskLoaderPanelUI.IsOpen);    
        }
    }

    private void ToggleClick(bool isOn)
    {
        if (isOn == true) 
        {
            _storageTaskLoaderPanelUI.Open();    
        }
        else
        {
            _storageTaskLoaderPanelUI.Close();
        }
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(ToggleClick);

        if (_storageTaskLoaderPanelUI != null) 
        {
            _storageTaskLoaderPanelUI.OnUpdateStatusOpen -= OnUpdateStatusOpen;
        }
    }
}
