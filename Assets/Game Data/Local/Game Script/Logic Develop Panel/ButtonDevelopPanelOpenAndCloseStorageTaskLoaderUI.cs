using UnityEngine;
using UnityEngine.UI;

public class ButtonDevelopPanelOpenAndCloseStorageTaskLoaderUI : MonoBehaviour
{
    [SerializeField]
    private Button _button;

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
            _button.onClick.AddListener(ButtonClick);
        }
    }

    private void ButtonClick()
    {
        if (_isOpen == true) 
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
        _button.onClick.RemoveListener(ButtonClick);  
    }
}
