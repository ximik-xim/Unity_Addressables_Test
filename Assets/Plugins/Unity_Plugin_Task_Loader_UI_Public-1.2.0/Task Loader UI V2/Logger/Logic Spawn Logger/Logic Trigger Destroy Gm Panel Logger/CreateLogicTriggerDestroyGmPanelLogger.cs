using System;
using UnityEngine;

/// <summary>
/// Логика создания триггера на уничтожение GM логера, когда будет уничтожена UI Task
/// </summary>
public class CreateLogicTriggerDestroyGmPanelLogger : MonoBehaviour
{
    private LogicDestroyTaskUI _destroyTaskUI;

    [SerializeField]
    private SpawnerLoggerPanelUI _spawnerLoggerPanelUI;
    
    [SerializeField] 
    private SpawnerTaskPanelUI _spawnerPaneTasklUI;
    
    [SerializeField] 
    private GetDataSODataDKODataKey _getKey;

    [SerializeField]
    private LogicTriggerDestroyGmPanelLogger _logicTrigger;
    
    private void Awake()
    {
        _spawnerLoggerPanelUI.OnSpawn += OnSpawn;
    }

    private void OnSpawn()
    {
        if (_spawnerPaneTasklUI.IsInit == false)
        {
            _spawnerPaneTasklUI.OnInit -= OnInitSpawnerPanel;
            _spawnerPaneTasklUI.OnInit += OnInitSpawnerPanel;
            return;
        }


        InitSpawnerPanel();
    }

    private void OnInitSpawnerPanel()
    {
        if (_spawnerPaneTasklUI.IsInit == true)
        {
            _spawnerPaneTasklUI.OnInit -= OnInitSpawnerPanel;
            InitSpawnerPanel();
        }
    }

    private void InitSpawnerPanel()
    {
        var dko = _spawnerPaneTasklUI.GetTaskUI().GetDKO();
        var data= (DKODataInfoT<LogicDestroyTaskUI>)dko.KeyRun(_getKey.GetData());

        _destroyTaskUI = data.Data;

        _logicTrigger.SetData(data.Data, _spawnerLoggerPanelUI.GetTaskUI());
    }

    private void OnDestroy()
    {
        _spawnerLoggerPanelUI.OnSpawn -= OnSpawn;
    }
}
