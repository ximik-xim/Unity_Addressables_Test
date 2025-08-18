using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// примр исп. разных списков задач с немного разной логикой выполнени 
/// </summary>
public class ExampleStorageTaskExecutor : MonoBehaviour
{
    [SerializeField] 
    private TL_TaskExecutorSequence _task1;
    
    [SerializeField] 
    private TL_TaskReadyExecutorDontBuffer _task2;
    
    [SerializeField] 
    private TL_TaskReadyExecutorLocalBuffer _task3;
    
    [SerializeField] 
    private TL_TaskReadyExecutorBuffer _task4;

    void OnGUI()
    {
        var buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 50;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        int width = 0;
        int height = 0;
        int x = Screen.width;
        int y = Screen.height / 2;


        if (GUI.Button(new Rect(width, height, x, y), "Запуск списоков задач", buttonStyle) == true)
        {
            if (_task1.IsCompleted == true)
            {
                _task1.StartAction();
                Debug.Log("Запуск задач. Ждите");
            }
        }
    }

    private void Awake()
    {
        if (_task1.IsInit == false)
        {
            _task1.StartInit();
        }
        
        if (_task2.IsInit == false)
        {
            _task2.StartInit();
        }

        
        if (_task3.IsInit == false)
        {
            _task3.StartInit();
        }

        
        if (_task4.IsInit == false)
        {
            _task4.StartInit();
        }

        
        _task1.OnCompleted += OnCompletedTask;
    }

    private void OnCompletedTask()
    {
        Debug.Log("Выполнение тасок 1 списка прошло успешно");
        
        _task2.OnCompleted -= OnCompletedTask2;
        _task2.OnCompleted += OnCompletedTask2;
        
        if (_task2.IsCompleted == true)
        {
            _task2.StartAction();
        }
    }

    private void OnCompletedTask2()
    {
        Debug.Log("Выполнение тасок 2 списка прошло успешно");
        
        _task3.OnCompleted -= OnCompletedTask3;
        _task3.OnCompleted += OnCompletedTask3;
        
        if (_task3.IsCompleted == true)
        {
            _task3.StartAction();
        }
    }
    
    private void OnCompletedTask3()
    {
        Debug.Log("Выполнение тасок 3 списка прошло успешно");
        
        _task4.OnCompleted -= OnCompletedTask4;
        _task4.OnCompleted += OnCompletedTask4;
        
        if (_task4.IsCompleted == true)
        {
            _task4.StartAction();
        }
    }
    
    private void OnCompletedTask4()
    {
        Debug.Log("Выполнение тасок 4 списка прошло успешно");
    }
    private void OnInitTask()
    {
        Debug.Log("Инициализация тасок прошла успешно");
    }
    
    
    private void OnDestroy()
    {
        _task1.OnInit -= OnInitTask;
        _task1.OnCompleted -= OnCompletedTask;
    }
}
