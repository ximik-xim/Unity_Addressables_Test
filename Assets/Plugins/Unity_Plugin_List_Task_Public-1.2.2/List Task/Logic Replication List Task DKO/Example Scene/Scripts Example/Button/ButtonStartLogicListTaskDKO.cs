
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// По нажатию на кнопку запустит выполнения списка задач
/// </summary>
public class ButtonStartLogicListTaskDKO : MonoBehaviour
{
   
    [SerializeField]
    private LogicListTaskDKO _listTask;

    [SerializeField]
    private DKOKeyAndTargetAction _dko;

    [SerializeField]
    private Button _button;
    
    private void Awake()
    {
        if (_listTask.IsInit == false)
        {
            _listTask.OnInit += OnInitListTask;
        }
        
        
        CheckInit();
    }

    private void OnInitListTask()
    {
        if (_listTask.IsInit == true) 
        {
            _listTask.OnInit -= OnInitListTask;
            CheckInit();
        }
       
    }
    

    private void CheckInit()
    {
        if (_listTask.IsInit == true)
        {
            Init();
        }
    }

    private void Init()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        _listTask.StartAction(_dko);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
}
