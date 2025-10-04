using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// По идеи он загрузит список сцен через Addressables
/// </summary>
public class GetKeyNameSceneStorageNameSceneAddressables : AbsGetStorageKeyNameScene
{
   public override bool IsInit => _isInit;
   private bool _isInit = false;
   public override event Action OnInit;

   [SerializeField]
   private AssetReference _key;
   
   [SerializeField] 
   private AbsCallbackGetDataTAddressables _getDataAddressables;

   private SO_Data_NameScene _localData;
   
   private void Awake()
   {
      if (_getDataAddressables.IsInit == false)
      {
         Debug.Log("Ожид. Иниц  get");
         _getDataAddressables.OnInit += OnInitGetData;
         return;
      }

      InitGetData();

   }
   private void OnInitGetData()
   {
      if (_getDataAddressables.IsInit == true) 
      {
         Debug.Log("законч ожит иниц  get");
         _getDataAddressables.OnInit -= OnInitGetData;
         InitGetData();
      }
      
   }
   
   private void InitGetData()
   {
      Debug.Log("Послан запрос на получения данных GameObject");
      var dataCallback = _getDataAddressables.GetData<SO_Data_NameScene>(_key);

      if (dataCallback.IsGetDataCompleted == true)
      {
         CompletedGetData();
      }
      else
      {
         dataCallback.OnGetDataCompleted += OnCompletedGetData;
      }
      
      void OnCompletedGetData()
      {
         if (dataCallback.IsGetDataCompleted == true)
         {
            dataCallback.OnGetDataCompleted -= OnCompletedGetData;
            CompletedGetData();
         }
      }

      void CompletedGetData()
      {
         if (dataCallback.StatusServer == StatusCallBackServer.Ok) 
         {
            _localData = dataCallback.GetData;
         
            _isInit = true;
            OnInit?.Invoke();
         }
         else
         {
            Debug.LogError("Ошибка, при загрузки хранилеща со списком сцен из Addrassable");
         }

      }

   }
   
   public override List<KeyNameScene> GetData()
   {
      return _localData.GetAllData();
   }
}
