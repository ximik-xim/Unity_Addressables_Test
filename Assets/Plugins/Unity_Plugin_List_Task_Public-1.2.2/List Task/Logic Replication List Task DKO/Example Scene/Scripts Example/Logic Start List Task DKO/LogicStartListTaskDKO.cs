using System;
using UnityEngine;

/// <summary>
/// Нужен в случае если надо запустить список задач в опр. методе
/// </summary>
public class LogicStartListTaskDKO : MonoBehaviour
{
    [SerializeField]
    private LogicListTaskDKO _listTask;

    [SerializeField] 
    private DKOKeyAndTargetAction _dkoData;

    [SerializeField]
    private bool _startTaskAwake;
    
    [SerializeField]
    private bool _startTaskEnable;

    [SerializeField]
    private bool _startTaskDisable;
    
    [SerializeField]
    private bool _startTaskDestroy;
    
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
        if (_startTaskAwake == true)
        {
            _listTask.StartAction(_dkoData);
        }
    }


    private void OnEnable()
    {
        if (_startTaskEnable == true && _listTask.IsInit == true) 
        {
            _listTask.StartAction(_dkoData);
        }
    }

    private void OnDisable()
    {
        if (_startTaskDisable == true && _listTask.IsInit == true) 
        {
            _listTask.StartAction(_dkoData);
        }
    }

    private void OnDestroy()
    {
        if (_startTaskDestroy == true && _listTask.IsInit == true) 
        {
            _listTask.StartAction(_dkoData);
        }
    }
}
