using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен если нужно проверить заблокир или разблокир указ уровень
/// И выполнить список действий(если заблокирован или разблокирован уровень)
/// </summary>
public class ActionListCheckIsBlockSceneLevel : MonoBehaviour
{
    public event Action OnInit;
    public bool IsInit => _isInit;
    private bool _isInit = false;
    
    [SerializeField]
    private GetPatchIntStorageBlockScene _getPatchIntStorageBlockScene;

    [SerializeField]
    private LogicListTaskDKO _listTaskSceneBlock;
    
    [SerializeField]
    private LogicListTaskDKO _listTaskSceneNotBlock;

    private StorageBlockScene _storageBlockScene;

    private KeyNameScene _keyNameScene;

    [SerializeField]
    private DKOKeyAndTargetAction _dko;

    private bool _isAutoCheckBlock = true;
    
    private void Awake()
    {
        if (_getPatchIntStorageBlockScene.IsInit == false)
        {
            _getPatchIntStorageBlockScene.OnInit += OnInitGetDkoPatch;
        }
        
        if (_listTaskSceneBlock.IsInit == false)
        {
            _listTaskSceneBlock.OnInit += OnInitListTaskSceneBlock;
        }
        
        if (_listTaskSceneNotBlock.IsInit == false)
        {
            _listTaskSceneNotBlock.OnInit += OnInitListTaskSceneNotBlock;
        }
        
        CheckInit();
    }

    private void OnInitListTaskSceneBlock()
    {
        if (_listTaskSceneBlock.IsInit == true)
        {
            _listTaskSceneBlock.OnInit -= OnInitListTaskSceneBlock;
            CheckInit();
        }
    }
    
    private void OnInitListTaskSceneNotBlock()
    {
        if (_listTaskSceneNotBlock.IsInit == true)
        {
            _listTaskSceneNotBlock.OnInit -= OnInitListTaskSceneNotBlock;
            CheckInit();
        }
    }

    private void OnInitGetDkoPatch()
    {
        if (_getPatchIntStorageBlockScene.IsInit == true)
        {
            _getPatchIntStorageBlockScene.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_isInit == false)
        {
            if (_getPatchIntStorageBlockScene.IsInit == true && _keyNameScene != null && _listTaskSceneBlock.IsInit == true && _listTaskSceneNotBlock.IsInit == true) 
            {
                _storageBlockScene = _getPatchIntStorageBlockScene.GetStorageBlockScene();
                
                _isInit = true;
                OnInit?.Invoke();

                SetAutoCheckBlock(_isAutoCheckBlock);
                
                StartCheck();
            }
        }
    }

    public void SetNameKey(KeyNameScene keyNameScene, bool isAutoCheckBlock = true)
    {
        _keyNameScene = keyNameScene;

        if (_getPatchIntStorageBlockScene.IsInit==true)
        {
            SetAutoCheckBlock(isAutoCheckBlock);
        }
        else
        {
            _isAutoCheckBlock = isAutoCheckBlock;
        }
        
        CheckInit();
    }

    public void SetAutoCheckBlock(bool isAutoCheckBlock)
    {
        if (isAutoCheckBlock == true) 
        {
            _getPatchIntStorageBlockScene.GetStorageBlockScene().OnUpdateData -= OnUpdateData;
            _getPatchIntStorageBlockScene.GetStorageBlockScene().OnUpdateData += OnUpdateData;
        }
        else
        {
            _getPatchIntStorageBlockScene.GetStorageBlockScene().OnUpdateData -= OnUpdateData;
        }

        _isAutoCheckBlock = isAutoCheckBlock;
    }

    private void OnUpdateData()
    {
        StartCheck();
    }

    public void StartCheck()
    {
        if (_keyNameScene != null)
        {
            if (_storageBlockScene.GetIsBlock(_keyNameScene) == true)  
            {
                _listTaskSceneBlock.StartAction(_dko);
            }
            else
            {
                _listTaskSceneNotBlock.StartAction(_dko);
            }
        }
        else
        {
            Debug.Log("ВНИМАНИЕ, ОШИБКА. Была вызвана проверка на заблокирована ли сцена, НО ПРИ ЭТО КЛЮЧ == null ");
        }
    }

    private void OnDestroy()
    {
        if (_getPatchIntStorageBlockScene != null) 
        {
            _getPatchIntStorageBlockScene.GetStorageBlockScene().OnUpdateData -= OnUpdateData;
        }
    }
}
