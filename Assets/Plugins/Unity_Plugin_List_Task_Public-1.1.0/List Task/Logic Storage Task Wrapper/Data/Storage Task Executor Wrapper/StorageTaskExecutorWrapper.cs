
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
/// <summary>
/// примр исп. разных списков задач(С оберткой Mono) с немного разной логикой выполнени 
/// </summary>
public class StorageTaskExecutorWrapper : MonoBehaviour
{
    /// <summary>
    /// Тут выбираем обертку
    /// </summary>
    [SerializeField] 
    private TL_TaskWrapperMono _wrapperTaskExecutor;
    
    private void Awake()
    {
        _wrapperTaskExecutor.OnInit += OnInitTask;
        _wrapperTaskExecutor.OnCompleted += OnCompletedTask;
    }
    
    void OnGUI()
    {
        var buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 40;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        int width = 0;
        int height = Screen.height / 2;
        int x = Screen.width;
        int y = Screen.height / 3;


        if (GUI.Button(new Rect(width, height, x, y), "Запуск списка(обертка Mono) задач", buttonStyle) == true)
        {
            if (_wrapperTaskExecutor.IsCompleted == true)
            {
                _wrapperTaskExecutor.StartAction();
                Debug.Log("Запуск задач. Ждите");
            }
        }
    }

    private void OnCompletedTask()
    {
        Debug.Log("Выполнение тасок прошло успешно");
    }

    private void OnInitTask()
    {
        Debug.Log("Инициализация тасок прошла успешно");
    }
    

    private void OnDestroy()
    {
        _wrapperTaskExecutor.OnInit -= OnInitTask;
        _wrapperTaskExecutor.OnCompleted -= OnCompletedTask;
    }
}
