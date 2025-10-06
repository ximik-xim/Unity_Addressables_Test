using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен что бы упростить доступ к логике блокировки
/// </summary>
public class GetPatchIntStorageBlockScene : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private GetDKOPatch _getDkoPatch;
    
    private StorageBlockScene _storageBlockScene;
    
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
            _storageBlockScene = _getDkoPatch.GetDKO<DKODataInfoT<StorageBlockScene>>().Data;
            
            if (_storageBlockScene.IsInit == false)
            {
                _storageBlockScene.OnInit += OnInitStorageBlockScene;
            }
            else
            {
                InitStorageBlockScene();
            }
        }
    }
    
    private void OnInitStorageBlockScene()
    {
        if (_storageBlockScene.IsInit == true)
        {
            _storageBlockScene.OnInit -= OnInitStorageBlockScene;
            InitStorageBlockScene();
        }
    }

    private void InitStorageBlockScene()
    {
        _isInit = true;
        OnInit?.Invoke();
    }

    public void SetStatus(KeyNameScene keyNameScene, bool isBlock)
    {
        _storageBlockScene.SetStatusBlock(keyNameScene, isBlock);
    }
    
    public void SetStatus(List<AbsKeyData<KeyNameScene, bool>> data)
    {
        _storageBlockScene.SetStatusBlock(data);
    }
}
