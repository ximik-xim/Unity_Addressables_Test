using System;
using UnityEngine;

public class GetUIGamePanelInfoStorageInStorage : MonoBehaviour
{
   private bool _isInit = false;
   public bool IsInit => _isInit;
   public event Action OnInit;
   
   [SerializeField]
   private GetDKOPatch _patchStorageGamePanelInfo;

   [SerializeField] 
   private GetDataSO_KeyUIGamePanelInfo _keyPanel;
   private UIGamePanelInfo _panelInfo;
   
   private void Awake()
   {
      if (_patchStorageGamePanelInfo.Init == false)
      {
         _patchStorageGamePanelInfo.OnInit += OnInitPatchStoragePanel;
         return;
      }

      GetDataDKO();
   }

   private void OnInitPatchStoragePanel()
   {
      _patchStorageGamePanelInfo.OnInit -= OnInitPatchStoragePanel;
      GetDataDKO();
   }

   private void GetDataDKO()
   {
      var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
      UIGamePanelInfoStorage storagePanel = DKOData.Data;
      
      if (storagePanel.IsInit == false)
      {
         storagePanel.OnInit += OnInitStoragePanel;
         return;
      }

      Init();
      
      
   }

   private void OnInitStoragePanel()
   {
      var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
      UIGamePanelInfoStorage storagePanel = DKOData.Data;
      storagePanel.OnInit -= OnInitStoragePanel;
      
      Init();
   }

   private void Init()
   {
      var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
      UIGamePanelInfoStorage storagePanel = DKOData.Data;

      if (storagePanel.PanelIsInsert(_keyPanel.GetData()) == false)
      {
         storagePanel.OnUpdateData += OnUpdateDataStoragePanel;
         return;
      }
      else
      {
         _panelInfo = storagePanel.GetPanel(_keyPanel.GetData());
      }
      
      
      _isInit = true;
      OnInit?.Invoke();
   }

   private void OnUpdateDataStoragePanel()
   {
      var DKOData = (DKODataInfoT<UIGamePanelInfoStorage>)_patchStorageGamePanelInfo.GetDKO();
      UIGamePanelInfoStorage storagePanel = DKOData.Data;

      if (storagePanel.PanelIsInsert(_keyPanel.GetData()) == true)
      {
         storagePanel.OnUpdateData -= OnUpdateDataStoragePanel;
         
         _panelInfo = storagePanel.GetPanel(_keyPanel.GetData());
      
         _isInit = true;
         OnInit?.Invoke();   
      }
      
   }
   
   public UIGamePanelInfo GetPanel()
   {
      return _panelInfo;
   }
}
