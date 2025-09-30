using System;
using UnityEngine;

/// <summary>
/// Нужен, что бы скапировать ключи
/// из Scene MBS DKO
/// в Dont Destroy MBS DKO
/// </summary>
public class CopyKeySceneMBS_DKO : MonoBehaviour
{
    [SerializeField]
    private FindMBS_DKO_DontDestroy _findMbsDkoDontDestroy;

    [SerializeField]
    private SceneMBS_DKO _sceneMbsDko;

    private void Awake()
    {
        if (_findMbsDkoDontDestroy.Init == false)
        {
            _findMbsDkoDontDestroy.OnInit += OnInitFindMbsDkoDontDestroy;
        }

        if (_sceneMbsDko.IsInit == false)
        {
            _sceneMbsDko.OnInit += OnInitSceneMbsDko;
        }
        
        CheckInit();
    }
    
    private void OnInitFindMbsDkoDontDestroy()
    {
        if (_findMbsDkoDontDestroy.Init == true)
        {
            _findMbsDkoDontDestroy.OnInit -= OnInitFindMbsDkoDontDestroy;
            CheckInit();
        }
        
    }
    
    private void OnInitSceneMbsDko()
    {
        if (_findMbsDkoDontDestroy.Init == true)
        {
            _sceneMbsDko.OnInit -= OnInitSceneMbsDko;
            CheckInit();
        }
        
    }
    
    private void CheckInit()
    {
        if (_findMbsDkoDontDestroy.Init == true && _sceneMbsDko.IsInit == true) 
        {
            InitData();
        }
    }

    private void InitData()
    {
        Debug.Log("Копирую ключи из Scene MBS DKO в Dont Destroy MBS DKO");
        _findMbsDkoDontDestroy.GetDontDestroyMbsDko.CopyDataTargetStorage(_sceneMbsDko);
    }
}
