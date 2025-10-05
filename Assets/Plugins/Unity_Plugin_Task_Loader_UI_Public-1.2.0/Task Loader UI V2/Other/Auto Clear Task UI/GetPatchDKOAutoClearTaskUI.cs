using UnityEngine;

public class GetPatchDKOAutoClearTaskUI : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _getDkoPatchStorageTaskLoader;
    
    [SerializeField]
    private GetDKOPatch _getDkoPatchStorageDkoTaskLoaderUI;
    
    private StorageTaskLoader _storageTaskLoader;
    private StorageDKO_TaskLoaderUI _storageDkoTaskLoaderUI;

    [SerializeField]
    private GetDataSODataDKODataKey _keyGetDestroy;

    [SerializeField]
    private bool _isActive = true;
    
    private void Awake()
    {
        if (_getDkoPatchStorageTaskLoader.Init == false)
        {
            _getDkoPatchStorageTaskLoader.OnInit += OnInitGetDkoPatchStorageTaskLoader;
        }
        
        if (_getDkoPatchStorageDkoTaskLoaderUI.Init == false)
        {
            _getDkoPatchStorageDkoTaskLoaderUI.OnInit += OnInitGetDkoPatchStorageDkoTaskLoaderUI;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatchStorageTaskLoader()
    {
        if (_getDkoPatchStorageTaskLoader.Init == true)
        {
            _getDkoPatchStorageTaskLoader.OnInit -= OnInitGetDkoPatchStorageTaskLoader;
            CheckInit();
        }
    }
    
    private void OnInitGetDkoPatchStorageDkoTaskLoaderUI()
    {
        if (_getDkoPatchStorageDkoTaskLoaderUI.Init == true)
        {
            _getDkoPatchStorageDkoTaskLoaderUI.OnInit -= OnInitGetDkoPatchStorageDkoTaskLoaderUI;
            CheckInit();
        }
    }
   
    private void CheckInit()
    {
        if (_getDkoPatchStorageTaskLoader.Init == true && _getDkoPatchStorageDkoTaskLoaderUI.Init == true)  
        {
            InitData();
        }
    }

    private void InitData()
    {
        _storageTaskLoader = _getDkoPatchStorageTaskLoader.GetDKO<DKODataInfoT<StorageTaskLoader>>().Data;

        _storageDkoTaskLoaderUI = _getDkoPatchStorageDkoTaskLoaderUI.GetDKO<DKODataInfoT<StorageDKO_TaskLoaderUI>>().Data;
        //_getDkoPatchStorageDkoTaskLoaderUI.GetDKO();
        _storageTaskLoader.OnCompleted += OnCompleted;
    }

    private void OnCompleted()
    {
        if (_isActive == true)
        {
            var listTask = _storageDkoTaskLoaderUI.GetAllTask();

            int maxCount = listTask.Count;
            for (int i = 0; i < maxCount; i++)
            {
                var data = (DKODataInfoT<LogicDestroyTaskUI>)listTask[i].KeyRun(_keyGetDestroy.GetData());
                LogicDestroyTaskUI logicDestroyTaskUI = data.Data;
            
                _storageDkoTaskLoaderUI.RemoveTaskUI(listTask[i]);
            
                logicDestroyTaskUI.StartDestroyObject();
            
                i--;
                maxCount--;
            }
        }
    }

    public void SetActiveLogic(bool activeAutoClear)
    {
        _isActive = activeAutoClear;
    }

    private void OnDestroy()
    {
        _storageTaskLoader.OnCompleted -= OnCompleted;
    }
}
