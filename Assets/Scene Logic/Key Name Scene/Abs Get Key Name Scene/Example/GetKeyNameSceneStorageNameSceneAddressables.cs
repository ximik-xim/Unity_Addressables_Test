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

   private SO_Data_NameSceneAndKeyString _localData;
   
   /// <summary>
   /// сохранять ли список ключей(нужно что бы не плодить ненужные ключи по 100 раз) 
   /// </summary>
   [SerializeField]
   private bool _isSaveList = true;
    
   private List<KeyNameScene> _listKeyScene;
   
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
      var dataCallback = _getDataAddressables.GetData<SO_Data_NameSceneAndKeyString>(_key);

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
   
   private List<KeyNameScene> GetListKey()
   {
      List<KeyNameScene> list = new List<KeyNameScene>();

      foreach (var VARIABLE in _localData.GetAllData())
      {
         list.Add(new KeyNameScene(VARIABLE.GetKey()));
      }

      return list;
   }
   
   public override List<KeyNameScene> GetData()
   {
      if (_isSaveList == false)
      {
         return GetListKey();
      }
        
      return _listKeyScene;
   }
}
