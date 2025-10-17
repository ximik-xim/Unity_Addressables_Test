using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Заполняет именами хранилище с именами сцен и ключами по этим именам  
/// </summary>
public class AwakeInsertDataStorageNameSceneAndKeyNameScene : MonoBehaviour
{
    [SerializeField]
    private List<AbsGetNameSceneAndKeyNameScene> _getNameAndKeyScene;

    [SerializeField]
    private StorageNameSceneAndKeyNameScene _storageNameSceneAndKeyNameScene;
    
    private void Awake()
    {
        List<AbsGetNameSceneAndKeyNameScene> _buffer = new List<AbsGetNameSceneAndKeyNameScene>();
        bool _isStart = false;

        StartLogic();

        void StartLogic()
        {
            _isStart = true;

            foreach (var VARIABLE in _getNameAndKeyScene)
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
        foreach (var VARIABLE in _getNameAndKeyScene)
        {
            foreach (var VARIABLE2 in VARIABLE.GetData())
            {
                _storageNameSceneAndKeyNameScene.AddSceneKey(VARIABLE2.Key, VARIABLE2.Data);
            }
            
        }
    }
}
