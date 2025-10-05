using System;
using IngameDebugConsole;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDevelopPanelSetActiveLogger : MonoBehaviour
{
   [SerializeField]
   private Button _button;

   private DebugLogManager _logger;

   [SerializeField]
   private bool _isActiveGm;
   
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
         _button.onClick.AddListener(ButtonClick);
      }
   }

   private void ButtonClick()
   {
      _logger.gameObject.SetActive(_isActiveGm);   
   }

   private void OnDestroy()
   {
      _button.onClick.RemoveListener(ButtonClick);  
   }
}
