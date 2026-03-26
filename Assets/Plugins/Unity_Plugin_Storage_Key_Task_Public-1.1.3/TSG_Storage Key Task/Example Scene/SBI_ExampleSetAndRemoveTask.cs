using System;
using UnityEngine;

public class SBI_ExampleSetAndRemoveTask : MonoBehaviour
{
    [SerializeField] 
    private SBI_SetAndRemoveTask _setAndRemoveTask;

    private void Awake()
    {
        if (_setAndRemoveTask.IsInit == false)
        {
            _setAndRemoveTask.OnInit += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _setAndRemoveTask.OnInit -= OnInit;
        Init();
    }
    
    private void Init()
    {
        _setAndRemoveTask.SetTask();
        Debug.Log("Task была добавлена");
    }

    private void OnDestroy()
    {
        _setAndRemoveTask.RemoveTask();
        Debug.Log("Task была удалена");
    }
}
