using System;
using UnityEngine;

/// <summary>
/// Пример использования списка задач
/// (в аргументы для старта задач перед. ссылка на DKO, что бы задача могла взять через него доп. данные)
/// </summary>
public class TL_ExampleAwakeStartListTaskDKO : MonoBehaviour
{
    [SerializeField]
    private LogicListTaskDKO _listTask;

    [SerializeField] 
    private DKOKeyAndTargetAction _dkoData;
    
    private bool _isInit = false;

    private void Awake()
    {
        if (_listTask.IsInit == false)
        {
            _listTask.OnInit += OnInitListTask;
            return;
        }

        InitListTask();
    }

    private void OnInitListTask()
    {
        if (_listTask.IsInit == true)
        {
            _listTask.OnInit -= OnInitListTask;
            InitListTask();
        }
    }
    
    private void InitListTask()
    {
        Debug.Log("Список задач было иниц.");
        _isInit = true;

        _listTask.OnCompleted += OnCompleted;
    }

    private void OnCompleted()
    {
        Debug.Log("Выполнение списка задач с DKO закончено");
    }

    private void OnDestroy()
    {
        _listTask.OnCompleted -= OnCompleted;
    }

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


        if (GUI.Button(new Rect(width, height, x, y), "Запуск списка задач", buttonStyle) == true)
        {
            if (_isInit == true)
            {
                Debug.Log("Запуск списка задач с DKO");
                _listTask.StartAction(_dkoData);    
            }
            
        }
    }
}
