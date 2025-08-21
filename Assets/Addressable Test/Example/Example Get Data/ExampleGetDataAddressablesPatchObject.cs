using System;
using UnityEngine;

/// <summary>
/// Пример получения данных через Путь к обьекту
/// </summary>
public class ExampleGetDataAddressablesPatchObject : MonoBehaviour
{
   public bool IsInit => _isInit;
   private bool _isInit = false;
   public event Action OnInit;

   /// <summary>
   /// путь до обьекта
   /// </summary>
   [SerializeField] 
   private string _pathObject;

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
      var dataCallback = _getDataAddressables.GetData<GameObject>(_pathObject);

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
