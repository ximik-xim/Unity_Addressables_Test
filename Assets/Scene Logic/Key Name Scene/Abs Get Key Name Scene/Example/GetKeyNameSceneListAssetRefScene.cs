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
        _isInit = true;
        OnInit?.Invoke();
    }


    public override List<KeyNameScene> GetData()
    {
        List<KeyNameScene> list = new List<KeyNameScene>();
        
        foreach (var VARIABLE in _listKeyNameScene)
        {
            list.Add(VARIABLE.GetSceneName());
        }

        return list;
    }
}
