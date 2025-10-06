using System;
using IngameDebugConsole;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDevelopPanelSetActiveLogger : MonoBehaviour
{
   [SerializeField]
   private Toggle _toggle;

   private DebugLogManager _logger;
   
   [SerializeField]
   private GetDKOPatch _getDkoPatchLogger;
   private void Awake()
   {
      if (_getDkoPatchLogger.Init == false)
      {
         _getDkoPatchLogger.OnInit += OnInitGetDkoPatch;
      }
        
      CheckInit();
   }
    
   private void OnInitGetDkoPatch()
   {
      if (_getDkoPatchLogger.Init == true)
      {
         _getDkoPatchLogger.OnInit -= OnInitGetDkoPatch;
         CheckInit();
      }
   }
    
   private void CheckInit()
   {
      if (_getDkoPatchLogger.Init == true)  
      {
         _logger = _getDkoPatchLogger.GetDKO<DKODataInfoT<DebugLogManager>>().Data;
         
         _toggle.SetIsOnWithoutNotify(_logger.gameObject.activeSelf);
         
         _toggle.onValueChanged.AddListener(ToggleClick);
      }
   }

   private void ToggleClick(bool isOn)
   {
      _logger.gameObject.SetActive(isOn);   
   }
   
   private void OnDestroy()
   {
      _toggle.onValueChanged.RemoveListener(ToggleClick);
   }
}
