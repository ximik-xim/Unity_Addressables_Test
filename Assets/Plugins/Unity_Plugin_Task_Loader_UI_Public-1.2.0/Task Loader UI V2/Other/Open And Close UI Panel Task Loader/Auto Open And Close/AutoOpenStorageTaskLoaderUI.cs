using System;
using UnityEngine;

public class AutoOpenStorageTaskLoaderUI : MonoBehaviour
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
            
            _storageTaskLoader.OnUpdateGeneralStatuse += OnUpdateGeneralStatuse;
        }
    }

    private void OnUpdateGeneralStatuse(TypeStatusTaskLoad status)
    {
        if (status == TypeStatusTaskLoad.Start || status == TypeStatusTaskLoad.Load) 
        {
            _storageTaskLoaderPanelUI.Open();   
        }
    }
    
    private void OnDestroy()
    {
        _storageTaskLoader.OnUpdateGeneralStatuse -= OnUpdateGeneralStatuse;
    }

}
