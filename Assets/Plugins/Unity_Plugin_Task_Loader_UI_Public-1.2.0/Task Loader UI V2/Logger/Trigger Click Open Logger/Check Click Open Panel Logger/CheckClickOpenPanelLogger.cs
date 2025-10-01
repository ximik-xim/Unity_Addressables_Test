using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен что бы подписать открытие панели логера на event находящийся на панели Task UI
/// </summary>
public class CheckClickOpenPanelLogger : MonoBehaviour
{
   [SerializeField] 
   private SpawnerLoggerPanelUI _spawnPanelLogger;

   [SerializeField] 
   private SpawnerTaskPanelUI _spawnerPaneTasklUI;

   [SerializeField] 
   private GetDataSODataDKODataKey _getKey;

   private TriggerClickOpenLogger _data;
   
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
      _data = data.Data;
      
      data.Data.OnClick += OnButtonClick;
   }

   private void OnButtonClick()
   {
      _spawnPanelLogger.GetTaskUI().Open();
   }

   private void OnDestroy()
   {
      if (_data != null)
      {
         _data.OnClick -= OnButtonClick;
      }
      
      _spawnPanelLogger.OnSpawn -= OnSpawn;
   }
}
