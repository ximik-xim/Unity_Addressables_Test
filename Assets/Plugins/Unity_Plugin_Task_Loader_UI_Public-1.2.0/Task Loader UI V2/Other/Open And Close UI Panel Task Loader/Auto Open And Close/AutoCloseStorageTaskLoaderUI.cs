using UnityEngine;

public class AutoCloseStorageTaskLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private GetDKOPatch _patchStorageTaskLoaderPanelUI;
    [SerializeField] 
    private GetDKOPatch _patchStorageTaskLoader;
    
    private StorageTaskLoader _storageTaskLoader;
    private StorageTaskLoaderUI _storageTaskLoaderPanelUI;
    
    private void Awake()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == false)
        {
            _patchStorageTaskLoaderPanelUI.OnInit += OnInitPatchStorageTaskLoaderPanelUI;
        }
        
        if (_patchStorageTaskLoader.Init == false)
        {
            _patchStorageTaskLoader.OnInit += OnInitPatchStorageTaskLoader;
        }

        
        CheckInit();
    }
    
    private void OnInitPatchStorageTaskLoaderPanelUI()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == true)
        {
            _patchStorageTaskLoaderPanelUI.OnInit -= OnInitPatchStorageTaskLoaderPanelUI;
            CheckInit();
        }
    }
    
    private void OnInitPatchStorageTaskLoader()
    {
        if (_patchStorageTaskLoader.Init == true)
        {
            _patchStorageTaskLoader.OnInit -= OnInitPatchStorageTaskLoader;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_patchStorageTaskLoaderPanelUI.Init == true && _patchStorageTaskLoader.Init == true) 
        {
            _storageTaskLoader = _patchStorageTaskLoader.GetDKO<DKODataInfoT<StorageTaskLoader>>().Data;
            _storageTaskLoaderPanelUI = _patchStorageTaskLoaderPanelUI.GetDKO<DKODataInfoT<StorageTaskLoaderUI>>().Data;
            
            _storageTaskLoader.OnCompleted += OnCompletedTask;
        }
    }
    private void OnCompletedTask()
    {
        _storageTaskLoaderPanelUI.Close();
    }
    
    private void OnDestroy()
    {
        _storageTaskLoader.OnCompleted -= OnCompletedTask;
    }

}
