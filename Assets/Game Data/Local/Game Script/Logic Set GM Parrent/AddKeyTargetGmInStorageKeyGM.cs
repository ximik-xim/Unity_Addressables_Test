using UnityEngine;

/// <summary>
/// Добавляет указ. GM в хранилеще Key Storage GM
/// И может убрать этот GM из хранилеща при уничтожении этого скрипта
/// </summary>
public class AddKeyTargetGmInStorageKeyGM : MonoBehaviour
{
    /// <summary>
    /// Удалять ли запись из хранилеща при Destroy этого скрипта
    /// </summary>
    [SerializeField]
    private bool _isRemoveKeyGmDestroy;
    
    [SerializeField]
    private GetDKOPatch _getDkoPatch;
    
    [SerializeField]
    private GetDataSO_StorageKeyGM _keySetCanvas;
    
    [SerializeField]
    private GameObject _targetGm;
    
    private void Awake()
    {
        if (_getDkoPatch.Init == false)
        {
            _getDkoPatch.OnInit += OnInitGetDkoPatch;
        }
        
        CheckInit();
    }
    
    private void OnInitGetDkoPatch()
    {
        if (_getDkoPatch.Init == true)
        {
            _getDkoPatch.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
        
    }
   
    private void CheckInit()
    {
        if (_getDkoPatch.Init == true) 
        {
            InitData();
        }
    }

    private void InitData()
    {
        StorageKeyAndGM storageKeyAndGm = _getDkoPatch.GetDKO<DKODataInfoT<StorageKeyAndGM>>().Data;
        storageKeyAndGm.AddGM(_keySetCanvas.GetData(), _targetGm);
    }

    private void OnDestroy()
    {
        if (_isRemoveKeyGmDestroy)
        {
            if (_getDkoPatch.Init == true) 
            {
                StorageKeyAndGM storageKeyAndGm = _getDkoPatch.GetDKO<DKODataInfoT<StorageKeyAndGM>>().Data;
                var gm = storageKeyAndGm.GetGM(_keySetCanvas.GetData());
         
                storageKeyAndGm.RemoveGM(_keySetCanvas.GetData());
            }
        }
    }
}
