using UnityEngine;
using UnityEngine.UI;

public class ButtonDevelopPanelOpenAndCloseLevelScene : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private StorageAbsGetKeyScene _storageAbsGetKeyScene;
    
    [SerializeField]
    private StorageBlockScene _storageBlockScene;

    [SerializeField]
    private bool _allOpen;
   
    [SerializeField]
    private GetDKOPatch _patchStorageAbsGetKeyScene;

    private void Awake()
    {
        if (_storageBlockScene == null)
        {
            _patchStorageAbsGetKeyScene.gameObject.SetActive(true);
            if (_patchStorageAbsGetKeyScene.Init == false)
            {
                _patchStorageAbsGetKeyScene.OnInit += OnInitGetDkoPatch;
            }    
        }
        
        if (_storageAbsGetKeyScene.IsInit == false)
        {
            _storageAbsGetKeyScene.OnInit += OnInitStorageAbsGetKeyScene;
        }

        CheckInit();
    }

    private void OnInitGetDkoPatch()
    {
        if (_patchStorageAbsGetKeyScene.Init == true)
        {
            _patchStorageAbsGetKeyScene.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void OnInitStorageAbsGetKeyScene()
    {
        if (_storageAbsGetKeyScene.IsInit == true)
        {
            _storageAbsGetKeyScene.OnInit -= OnInitStorageAbsGetKeyScene;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_storageBlockScene == null)
        {
            if (_patchStorageAbsGetKeyScene.Init == true && _storageAbsGetKeyScene.IsInit == true) 
            {
                _storageBlockScene = _patchStorageAbsGetKeyScene.GetDKO<DKODataInfoT<StorageBlockScene>>().Data;
                _button.onClick.AddListener(ButtonClick);
            }
        }
        else if (_storageBlockScene != null && _storageAbsGetKeyScene.IsInit == true) 
        {
            _button.onClick.AddListener(ButtonClick);
        }
    }

    private void ButtonClick()
    {
        if (_allOpen == true) 
        {
            foreach (var VARIABLE in _storageAbsGetKeyScene.GetAllKeyScene())
            {
                _storageBlockScene.SetStatusBlock(VARIABLE, false);
            }
    
        }
        else
        {
            foreach (var VARIABLE in _storageAbsGetKeyScene.GetAllKeyScene())
            {
                _storageBlockScene.SetStatusBlock(VARIABLE, true);
            }
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);  
    }
}
