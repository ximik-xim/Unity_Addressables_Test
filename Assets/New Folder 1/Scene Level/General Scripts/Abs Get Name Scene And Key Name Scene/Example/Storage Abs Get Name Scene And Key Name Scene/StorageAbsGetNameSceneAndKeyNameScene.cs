using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Нужен для удобной работы с выбором хранилищ,
/// из которых буду брать список имен и ключей сцен
/// </summary>
public class StorageAbsGetNameSceneAndKeyNameScene : AbsGetNameSceneAndKeyNameScene
{
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
    
    [SerializeField] 
    private List<AbsGetNameSceneAndKeyNameScene> _storageSceneLevel;
    
    private void Awake()
    {
        List<AbsGetNameSceneAndKeyNameScene> _buffer = new List<AbsGetNameSceneAndKeyNameScene>();
        bool _isStart = false;

        StartLogic();

        void StartLogic()
        {
            _isStart = true;

            foreach (var VARIABLE in _storageSceneLevel)
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
        _isInit = true;
        OnInit?.Invoke();
    }
    
    public override List<AbsKeyData<string, KeyNameScene>> GetData()
    {
        List<AbsKeyData<string, KeyNameScene>> keyNameScenes = new List<AbsKeyData<string, KeyNameScene>>();
        
        //получ. список сцен
        foreach (var VARIABLE in _storageSceneLevel)
        {
            foreach (var VARIABLE2 in VARIABLE.GetData()) 
            {
                keyNameScenes.Add(VARIABLE2);
            }
        }

        return keyNameScenes;
    }
}
