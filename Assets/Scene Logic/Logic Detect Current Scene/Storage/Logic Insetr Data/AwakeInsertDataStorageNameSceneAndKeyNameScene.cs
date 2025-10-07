using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// Заполняет именами хранелеще со именами сцен и ключами по этим именам  
/// </summary>
public class AwakeInsertDataStorageNameSceneAndKeyNameScene : MonoBehaviour
{
    [SerializeField]
    private List<LogicGetNameSceneAndKeyInStorageReferenceScene> _storageKeyRefScene;

     [SerializeField]
     private List<SO_Data_NameSceneAndKeyString> _storageKeyNameScene;

    [SerializeField]
    private StorageNameSceneAndKeyNameScene _storageNameSceneAndKeyNameScene;
    
    private void Awake()
    {
        List<LogicGetNameSceneAndKeyInStorageReferenceScene> _buffer = new List<LogicGetNameSceneAndKeyInStorageReferenceScene>();
        bool _isStart = false;

        StartLogic();

        void StartLogic()
        {
            _isStart = true;

            foreach (var VARIABLE in _storageKeyRefScene)
            {
                if (VARIABLE.IsInit == false)
                {
                    _buffer.Add(VARIABLE);
                    VARIABLE.OnInit += CheckInit;
                }
            }

            _isStart = false;

            CheckInit();
        }

        void CheckInit()
        {
            if (_isStart == false)
            {
                int targetCount = _buffer.Count;
                for (int i = 0; i < targetCount; i++)
                {
                    if (_buffer[i].IsInit == true)
                    {
                        _buffer[i].OnInit -= CheckInit;
                        _buffer.RemoveAt(i);
                        i--;
                        targetCount--;
                    }
                }

                if (_buffer.Count == 0)
                {
                    Completed();
                }
            }
        }
    }

    private void Completed()
    {
        StartInsertSceneNameAndKey();
    }

    public void StartInsertSceneNameAndKey()
    {
        foreach (var VARIABLE in _storageKeyRefScene)
        {
            foreach (var VARIABLE2 in VARIABLE.GetListNameSceneAndKey)
            {
                _storageNameSceneAndKeyNameScene.AddSceneKey(VARIABLE2.Key, VARIABLE2.Data);
            }
            
        }

        foreach (var VARIABLE in _storageKeyNameScene)
        {
            foreach (var VARIABLE2 in VARIABLE.GetAllData())
            {
                _storageNameSceneAndKeyNameScene.AddSceneKey(VARIABLE2.GetNameScene(), new KeyNameScene(VARIABLE2.GetKey()));
            }
        }
    }

    
  
}
