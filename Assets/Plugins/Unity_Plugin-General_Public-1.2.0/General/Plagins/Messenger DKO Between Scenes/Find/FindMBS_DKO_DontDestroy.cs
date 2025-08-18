using System;
using UnityEngine;
/// <summary>
/// Этот скрипт, при переходе на другую сцену будет искать у новой сцены хранилеще с DKO
/// (по идеи) у каждой сцены будет свой экземпляр и когда перейдем на сцену где есть этот экземпляр, то через общее хранилеще с DKO(DontDestroyMBS_DKO)
/// </summary>
public class FindMBS_DKO_DontDestroy : MonoBehaviour
{
    private DontDestroyMBS_DKO _dontDestroyMbsDko;
    public DontDestroyMBS_DKO GetDontDestroyMbsDko => _dontDestroyMbsDko;

    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;
    private void OnEnable()
    {
        _dontDestroyMbsDko = FindObjectOfType<DontDestroyMBS_DKO>();
        if (_dontDestroyMbsDko.IsInit == false)
        {
            _dontDestroyMbsDko.OnInit += OnInitDontDestroy;
            return;
        }
        
        InitDontDestroy();
    }

    private void OnInitDontDestroy()
    {
        _dontDestroyMbsDko.OnInit -= OnInitDontDestroy;

        InitDontDestroy();
    }
    
    private void InitDontDestroy()
    {
        _init = true;
        OnInit?.Invoke();
    }
}
