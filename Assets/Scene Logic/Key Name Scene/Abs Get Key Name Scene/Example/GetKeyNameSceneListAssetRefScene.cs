using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен если нужно получить не все элементы из хранилеща, а только определённые
/// </summary>
public class GetKeyNameSceneListAssetRefScene : AbsGetStorageKeyNameScene
{
    [SerializeField]
    private List<KeyNameSceneInGetDataSO_KeyReferenceScene> _listKeyNameScene;
    
    public override bool IsInit => _isInit;
    private bool _isInit = false;
    public override event Action OnInit;
    
    /// <summary>
    /// сохранять ли список ключей(нужно что бы не плодить ненужные ключи по 100 раз) 
    /// </summary>
    [SerializeField]
    private bool _isSaveList = true;
    
    private List<KeyNameScene> _listKeyScene;
    
    private void Awake()
    {
        List<KeyNameSceneInGetDataSO_KeyReferenceScene> _buffer = new List<KeyNameSceneInGetDataSO_KeyReferenceScene>();
        bool _isStart = false;

        StartLogic();
        
        void StartLogic()
        {
            _isStart = true;

            foreach (var VARIABLE in _listKeyNameScene)
            {
                VARIABLE.StartInit();
                
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
        if (_isSaveList == true) 
        {
            _listKeyScene = GetListKey();
        }

        
        _isInit = true;
        OnInit?.Invoke();
    }

    private List<KeyNameScene> GetListKey()
    {
        List<KeyNameScene> list = new List<KeyNameScene>();
        
        foreach (var VARIABLE in _listKeyNameScene)
        {
            list.Add(VARIABLE.GetKeySceneName());
        }

        return list;
    }

    public override List<KeyNameScene> GetData()
    {
        if (_isSaveList == false)
        {
            return GetListKey();
        }
        
        return _listKeyScene;
    }
}
