using System;
using UnityEngine;

/// <summary>
/// Пример получения данных через GUID обьекта
/// </summary>
public class ExampleGetDataAddressablesGUID : MonoBehaviour
{
   public bool IsInit => _isInit;
   private bool _isInit = false;
   public event Action OnInit;

   /// <summary>
   /// GUID обьекта
   /// </summary>
   [SerializeField] 
   private Hash128 _GUID;

   [SerializeField] 
   private AbsCallbackGetDataAddressables _getDataAddressables;

   private void Awake()
   {
      if (_getDataAddressables.IsInit == false)
      {
         _getDataAddressables.OnInit += OnInitGetData;
         return;
      }

      InitGetData();

   }

   private void OnInitGetData()
   {
      if (_getDataAddressables.IsInit == true)
      {
         _getDataAddressables.OnInit -= OnInitGetData;
         InitGetData();
      }

   }

   private void InitGetData()
   {
      Debug.Log("Послан запрос на получения данных GameObject");
      var dataCallback = _getDataAddressables.GetData<GameObject>(_GUID);

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
         Debug.Log("----- Данные получены ----");
         Debug.Log("Статус запроса = " + dataCallback.StatusServer.ToString());
         Debug.Log("Получен обьект = " + dataCallback.GetData);
         Debug.Log("Проверка на null = " + (dataCallback.GetData == null));

         _isInit = true;
         OnInit?.Invoke();
      }

   }
}
