using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика создания триггера на включение панели логгера, когда будет начата кнопка на UI Task
/// </summary>
public class CreatorLogicTriggerButtonOpenPanelLogger : MonoBehaviour
{
   [SerializeField] 
   private SpawnerLoggerPanelUI _spawnPanelLogger;

   [SerializeField] 
   private SpawnerTaskPanelUI _spawnerPaneTasklUI;

   [SerializeField] 
   private GetDataSODataDKODataKey _getKey;

   [SerializeField]
   private LogicTriggerButtonOpenPanelLogger _logicTrigger;
   
   private void Awake()
   {
      _spawnPanelLogger.OnSpawn += OnSpawn;
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
      var data = (DKODataInfoT<TriggerClickOpenLogger>)dko.KeyRun(_getKey.GetData());
      
      _logicTrigger.SetData(data.Data, _spawnPanelLogger.GetTaskUI());
   }
   

   private void OnDestroy()
   {
      _spawnPanelLogger.OnSpawn -= OnSpawn;
   }
}
